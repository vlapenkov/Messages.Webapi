import { Constructor } from '@/app/@types/constructor';
import { extend } from '@/app/core/handlers/base/handler-lab';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { onMounted } from 'vue';
import { ICollectionHttpService } from '../../../http/custom/collection.http-service';
import { parse, parseArray } from '../../../http/handlers/parse.handlers';
import { createDefaultStore, DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getCollectionProp } from '../../state/decorators/property-keys/collection.prop-key';
import { getDataStatusProp } from '../../state/decorators/property-keys/data-status.prop-key';
import {
  getSelectedItemPropKey,
  getSelectedItemPropOptions,
} from '../../state/decorators/property-keys/selected-item.prop-key';
import { ISelectedItemOptions } from '../../state/decorators/selected-item.decorator';
import { DataStatus } from '../../tools/data-status';
import { Creation, Edititng, NotValidData } from '../../tools/not-valid-data';
import { ICollectionStoreAdd } from './@types/ICollectionstoreAdd';
import { ICollectionStoreDelete } from './@types/ICollectionStoreDelete';
import { ICollectionStoreEdit } from './@types/ICollectionstoreEdit';
import { ICollectionStoreRead } from './@types/ICollectionStoreRead';
import { ICollectionStoreSelectedItem } from './@types/ICollectionStoreSelectedItem';

export function defineCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends StateBase,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<TState>,
  service: ICollectionHttpService<TIModel>,
) {
  const stateDefault = new State();
  const store: DefaultStore<TState> = createDefaultStore(name, stateDefault);
  const collectionKey = getCollectionProp(stateDefault);
  if (collectionKey == null) {
    throw new Error('Cannot create collection store from non-collection state');
  }
  const { computeState, action, mutation } = store;

  const itemsDumb = computeState((state) => state[collectionKey] as unknown as TModel[] | null);

  const dataStatusKey = getDataStatusProp(stateDefault);

  if (dataStatusKey == null) {
    throw new Error('Please, provide @dataStatus for collection state');
  }

  const status = computeState((state) => state[dataStatusKey] as unknown as DataStatus);

  const getDataAsyncActionKey = 'get-data-async';

  const getDataAsync: Action<{ force: boolean } | undefined, TModel[] | null> = action(
    getDataAsyncActionKey,
    async (ops: { force: boolean } = { force: false }) => {
      const currentStatus = status.value.status;
      if (!ops.force && currentStatus === 'loaded') {
        return itemsDumb.value;
      }

      status.value = new DataStatus(currentStatus === 'initial' ? 'loading' : 'updating');
      const requestFn = extend(service.get).pipe(parseArray(Model)).done();
      const response = await requestFn();
      if (response.status === HttpStatus.Success) {
        itemsDumb.value = response.data ?? null;
        status.value = new DataStatus('loaded');
      } else {
        status.value = new DataStatus('error', response.message);
      }

      return itemsDumb.value;
    },
  );

  const itemsSmart = (ops: { force: boolean } = { force: false }) => {
    onMounted(() => {
      if (ops.force || status.value.status === 'initial') {
        getDataAsync(ops);
      }
    });
    return itemsDumb;
  };

  const readolnlyCollectionStore: ICollectionStoreRead<TIModel, TModel> = {
    status,
    items: itemsSmart,
    getDataAsync,
  };

  const selectedItemKey = getSelectedItemPropKey(stateDefault);
  if (selectedItemKey == null) {
    return readolnlyCollectionStore;
  }
  const itemOptions = getSelectedItemPropOptions(
    selectedItemKey as string,
    stateDefault,
  ) as unknown as ISelectedItemOptions | undefined;

  if (itemOptions == null) {
    console.error(stateDefault, itemOptions);
    throw new Error('Options value (ISelectedItemOptions) must be provided!');
  }
  if (!itemOptions.create && !itemOptions.update && itemOptions.delete) {
    return readolnlyCollectionStore;
  }

  const itemSelected = computeState(
    (state) => state[selectedItemKey] as unknown as NotValidData<TModel> | null,
  );

  let extended: ICollectionStoreSelectedItem<TIModel, TModel> = {
    itemSelected,
  };

  if (itemOptions.update) {
    const selectItem = mutation<string | number | symbol>('select-item', (_, key) => {
      const selected = itemsDumb.value?.find((i) => i.key === key);
      if (selected == null) {
        return;
      }
      itemSelected.value = new Edititng({ ...selected });
    });
    extended = { ...extended, selectItem } as ICollectionStoreSelectedItem<TIModel, TModel> &
      ICollectionStoreEdit;
  }

  if (itemOptions.create) {
    const createItem = mutation<void>('create-item', () => {
      itemSelected.value = new Creation(new Model());
    });
    extended = {
      ...extended,
      createItem,
    } as ICollectionStoreSelectedItem<TIModel, TModel> & ICollectionStoreAdd;
  }

  const saveChanges: Action<void> = action('save-changes', async () => {
    if (itemSelected.value == null) {
      return;
    }
    const { mode, data: itemToSave } = itemSelected.value;
    if (mode === 'create') {
      const { status: responseStatus, data: itemToAdd } = await parse(Model)(
        service.post(itemToSave.toRequest()),
      );
      if (responseStatus === HttpStatus.Success && itemToAdd != null) {
        itemsDumb.value = [
          ...(itemsDumb.value ?? []).filter((i) => i.key !== itemToAdd.key),
          itemToAdd,
        ];
      }
    } else if (mode === 'edit') {
      const { status: responseStatus, data: itemToAdd } = await parse(Model)(
        service.post(itemToSave.toRequest()),
      );
      if (responseStatus === HttpStatus.Success && itemToAdd != null) {
        itemsDumb.value = [
          ...(itemsDumb.value ?? []).filter((i) => i.key !== itemToAdd.key),
          itemToAdd,
        ];
      }
    }
  });

  if (itemOptions.delete) {
    const deleteItem: Action<string | number | symbol> = action<string | number | symbol>(
      'delete-item',
      async (key) => {
        if (itemsDumb.value == null) {
          return;
        }
        const index = itemsDumb.value.findIndex((i) => i.key === key);
        if (index === -1) {
          return;
        }
        const { status: deletionStatus } = await service.del(itemsDumb.value[index].toRequest());
        if (deletionStatus === HttpStatus.Success) {
          itemsDumb.value = itemsDumb.value.splice(index, 1);
        }
      },
    );
    extended = { ...extended, deleteItem } as ICollectionStoreSelectedItem<TIModel, TModel> &
      ICollectionStoreDelete;
  }

  const editableCollectionStore = {
    ...readolnlyCollectionStore,
    ...extended,
    saveChanges,
  };

  return editableCollectionStore;
}
