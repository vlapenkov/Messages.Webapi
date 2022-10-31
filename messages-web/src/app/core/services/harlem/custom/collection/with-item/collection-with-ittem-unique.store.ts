import { Constructor } from '@/app/@types/constructor';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModelUnique } from '@/app/core/models/@types/IModel';
import { UniqueModel } from '@/app/core/models/unique.model';
import { ICollectionHttpService } from '@/app/core/services/http/custom/collection.http-service';
import { parse } from '@/app/core/services/http/handlers/parse.handlers';
import type { Mutation } from '@harlem/core';
import type { Action } from '@harlem/extension-action';
import { Creation, Edititng } from '../../../tools/not-valid-data';
import { CollectionWithItemState } from './collection-with-item.state';
import {
  createCollectionWithItemStore,
  ICollectionWithItemStore,
} from './collection-with-item.store';

export interface ICollectionWithItemUniqueStore<
  Tid extends string | number | symbol,
  TIModel extends IModelUnique<Tid>,
  TModel extends UniqueModel<Tid, TIModel>,
> extends ICollectionWithItemStore<TIModel, TModel> {
  selectItem: Mutation<Tid, void>;
  createItem: (payload?: void | undefined) => void;
  saveChanges: Action<void>;
}

export function createCollectionWithItemUniqueStore<
  TId extends string | number | symbol,
  TIModel extends IModelUnique<TId>,
  TModel extends UniqueModel<TId, TIModel>,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<CollectionWithItemState<TModel>>,
  service: ICollectionHttpService<TIModel>,
) {
  const [store, baseStore] = createCollectionWithItemStore(name, Model, State, service);
  const { mutation, action } = store;

  const { items, itemSelected } = baseStore;

  const selectItem = mutation<TId>('select-item', (state, id) => {
    const selected = items.value?.find((i) => i.id === id);
    if (selected == null) {
      return;
    }
    state.itemSelected = new Edititng({ ...selected });
  });

  const createItem = mutation<void>('create-item', (state) => {
    state.itemSelected = new Creation(new Model());
  });

  const saveChanges = action('save-changes', async () => {
    if (itemSelected.value == null) {
      return;
    }
    const { mode, data: itemToSave } = itemSelected.value;
    if (mode === 'create') {
      const { status, data: itemToAdd } = await parse(Model)(service.post(itemToSave.asObject()));
      if (status === HttpStatus.Success && itemToAdd != null) {
        items.value = [...(items.value ?? []).filter((i) => !i.compareId(itemToAdd)), itemToAdd];
      }
    } else if (mode === 'edit') {
      const { status, data: itemToAdd } = await parse(Model)(service.post(itemToSave.asObject()));
      if (status === HttpStatus.Success && itemToAdd != null) {
        items.value = [...(items.value ?? []).filter((i) => !i.compareId(itemToAdd)), itemToAdd];
      }
    }
  });

  const extended: ICollectionWithItemUniqueStore<TId, TIModel, TModel> = {
    ...baseStore,
    selectItem,
    createItem,
    saveChanges,
    itemSelected,
  } as const;

  return [store, extended] as const;
}
