import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { WritableComputedRef } from 'vue';
import type { Mutation } from '@harlem/core';
import type { Action } from '@harlem/extension-action';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { parse } from '@/app/core/services/http/handlers/parse.handlers';
import { ICollectionHttpService } from '../../../../http/custom/collection.http-service';
import { CollectionEditableState } from './collection-editable.state';
import {
  createCollectionReadonlyStore,
  IReadonlyCollectionStore,
} from '../readonly/collection-readonly.store';
import { Creation, Edititng, NotValidData } from '../../../tools/not-valid-data';
import { DefaultStore } from '../../../harlem.service';

export interface ICollectionEditableStore<TIModel extends IModel, TModel extends ModelBase<TIModel>>
  extends IReadonlyCollectionStore<TIModel, TModel> {
  itemSelected: WritableComputedRef<NotValidData<TModel> | null>;
  selectItem: Mutation<string | number | symbol, void>;
  createItem: (payload?: void | undefined) => void;
  saveChanges: Action<void>;
}

export function createCollectionEditableStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<CollectionEditableState<TModel>>,
  service: ICollectionHttpService<TIModel>,
): [DefaultStore<CollectionEditableState<TModel>>, ICollectionEditableStore<TIModel, TModel>] {
  const [store, readonlyStore] = createCollectionReadonlyStore(name, Model, State, service);
  const { computeState } = store;

  const itemSelected = computeState((state) => state.itemSelected);

  const { items } = readonlyStore;

  const { mutation, action } = store;

  const selectItem = mutation<string | number | symbol>('select-item', (state, key) => {
    const selected = items.value?.find((i) => i.key === key);
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
      const { status, data: itemToAdd } = await parse(Model)(service.post(itemToSave.toRequest()));
      if (status === HttpStatus.Success && itemToAdd != null) {
        items.value = [...(items.value ?? []).filter((i) => i.key !== itemToAdd.key), itemToAdd];
      }
    } else if (mode === 'edit') {
      const { status, data: itemToAdd } = await parse(Model)(service.post(itemToSave.toRequest()));
      if (status === HttpStatus.Success && itemToAdd != null) {
        items.value = [...(items.value ?? []).filter((i) => i.key !== itemToAdd.key), itemToAdd];
      }
    }
  });

  const extended = {
    ...readonlyStore,
    itemSelected,
    selectItem,
    createItem,
    saveChanges,
  } as const;

  return [store, extended];
}
