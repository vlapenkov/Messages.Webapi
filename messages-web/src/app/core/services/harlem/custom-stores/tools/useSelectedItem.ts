import { Constructor } from '@/app/@types/constructor';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { WritableComputedRef } from 'vue';
import { parse } from '../../../http/handlers/parse.handlers';
import { DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import {
  getSelectedItemPropKey,
  getSelectedItemPropOptions,
} from '../../state/decorators/property-keys/selected-item.prop-key';
import { ISelectedItemOptions } from '../../state/decorators/selected-item.decorator';
import { Creation, Edititng, NotValidData } from '../../tools/not-valid-data';
import { IModelPostPut } from './@types/IModelPostPut';

/** Добавляетт в стору, где есть коллекция
 * возможность добавления, изменения удаления элемента этой коллекции
 */
export function useSelectedItem<
  TState extends StateBase,
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
>(
  store: DefaultStore<TState>,
  Model: Constructor<TModel>,
  service: IModelPostPut<TIModel>,
  itemsDumb: WritableComputedRef<TModel[] | null>,
) {
  const selectedItemKey = getSelectedItemPropKey(store.state);
  if (selectedItemKey == null) {
    return {};
  }
  const itemOptions = getSelectedItemPropOptions(
    selectedItemKey as string,
    store.state,
  ) as unknown as ISelectedItemOptions | undefined;

  if (itemOptions == null) {
    throw new Error('Options value (ISelectedItemOptions) must be provided!');
  }

  if (!itemOptions.create && !itemOptions.update && itemOptions.delete) {
    return {};
  }

  const itemSelected = store.computeState(
    (state) => state[selectedItemKey as keyof TState] as unknown as NotValidData<TModel> | null,
  );

  const saveChanges: Action<void> = store.action('save-changes', async () => {
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
        service.put(itemToSave.toRequest()),
      );
      if (responseStatus === HttpStatus.Success && itemToAdd != null) {
        itemsDumb.value = [
          ...(itemsDumb.value ?? []).filter((i) => i.key !== itemToAdd.key),
          itemToAdd,
        ];
      }
    }
  });

  const selectItem = !itemOptions.update
    ? undefined
    : store.mutation<string | number | symbol>('select-item', (_, key) => {
        const selected = itemsDumb.value?.find((i) => i.key === key);
        if (selected == null) {
          return;
        }
        itemSelected.value = new Edititng(selected.clone() as TModel);
      });
  const createItem = !itemOptions.create
    ? undefined
    : store.mutation<void>('create-item', () => {
        itemSelected.value = new Creation(new Model());
      });
  return { itemSelected, saveChanges, selectItem, createItem };
}
