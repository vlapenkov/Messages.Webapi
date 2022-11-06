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
  getSelectedItemPropOptionsKey,
} from '../../state/decorators/property-keys/selected-item.prop-key';
import { DataStatus } from '../../tools/data-status';
import { Creation, Edititng, NotValidData } from '../../tools/not-valid-data';
import { IReadonlyCollectionStore } from './readonly/collection-readonly.store';

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

  const items = computeState((state) => state[collectionKey] as TModel[] | null);

  const dataStatusKey = getDataStatusProp(stateDefault);

  if (dataStatusKey == null) {
    throw new Error('Please, provide @dataStatus for collection state');
  }

  const status = computeState((state) => state[dataStatusKey] as DataStatus);

  const getDataAsyncActionKey = 'get-data-async';

  const getDataAsyncAction: Action<{ force: boolean } | undefined, TModel[] | null> = action(
    getDataAsyncActionKey,
    async (ops: { force: boolean } = { force: false }) => {
      const currentStatus = status.value.status;
      if (!ops.force && currentStatus === 'loaded') {
        return items.value;
      }

      status.value = new DataStatus(currentStatus === 'initial' ? 'loading' : 'updating');
      const requestFn = extend(service.get).pipe(parseArray(Model)).done();
      const response = await requestFn();
      if (response.status === HttpStatus.Success) {
        items.value = response.data ?? null;
        status.value = new DataStatus('loaded');
      } else {
        status.value = new DataStatus('error', response.message);
      }

      return items.value;
    },
  );

  const itemsAsync = (ops: { force: boolean } = { force: false }) => {
    onMounted(() => {
      if (ops.force || status.value.status === 'initial') {
        getDataAsyncAction(ops);
      }
    });
    return items;
  };

  const readolnlyCollectionStore: IReadonlyCollectionStore<TIModel, TModel> = {
    status,
    itemsAsync,
    getDataAsyncAction,
    items,
  };

  const selectedItemKey = getSelectedItemPropKey(stateDefault);
  if (selectedItemKey == null) {
    return readolnlyCollectionStore;
  }
  const selectedItemOptionsKey = getSelectedItemPropOptionsKey(
    selectedItemKey as string,
    stateDefault,
  );

  if (selectedItemOptionsKey == null) {
    throw new Error('Options must be provided!');
  }

  const itemSelected = computeState(
    (state) => state[selectedItemKey] as NotValidData<TModel> | null,
  );

  const selectItem = mutation<string | number | symbol>('select-item', (_, key) => {
    const selected = items.value?.find((i) => i.key === key);
    if (selected == null) {
      return;
    }
    itemSelected.value = new Edititng({ ...selected });
  });

  const createItem = mutation<void>('create-item', () => {
    itemSelected.value = new Creation(new Model());
  });

  const saveChanges = action('save-changes', async () => {
    if (itemSelected.value == null) {
      return;
    }
    const { mode, data: itemToSave } = itemSelected.value;
    if (mode === 'create') {
      const { status: responseStatus, data: itemToAdd } = await parse(Model)(
        service.post(itemToSave.toRequest()),
      );
      if (responseStatus === HttpStatus.Success && itemToAdd != null) {
        items.value = [...(items.value ?? []).filter((i) => i.key !== itemToAdd.key), itemToAdd];
      }
    } else if (mode === 'edit') {
      const { status: responseStatus, data: itemToAdd } = await parse(Model)(
        service.post(itemToSave.toRequest()),
      );
      if (responseStatus === HttpStatus.Success && itemToAdd != null) {
        items.value = [...(items.value ?? []).filter((i) => i.key !== itemToAdd.key), itemToAdd];
      }
    }
  });

  const editableCollectionStore = {
    ...readolnlyCollectionStore,
    itemSelected,
    selectItem,
    createItem,
    saveChanges,
  };

  return editableCollectionStore;
}
