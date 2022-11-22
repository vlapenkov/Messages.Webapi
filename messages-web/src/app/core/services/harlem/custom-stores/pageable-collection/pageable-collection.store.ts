import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IPageableCollectionHttpServie } from '../../../http/custom/pageable-collection.http-service';
import { DefaultStore, createDefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { useDataStatus } from '../composables/data-status.composable';
import { usePages } from '../composables/pages.composable';
import { useSelectedItemForCollection } from '../composables/selected-item.composable';
import { useTriggers } from '../composables/triggers.composable';
import { IPageableCollectionStore } from './@types/IPageableCollectionStore';

export function definePageableCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends StateBase,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<TState>,
  service: IPageableCollectionHttpServie<TIModel>,
) {
  const stateDefault = new State();
  const store: DefaultStore<TState> = createDefaultStore(name, stateDefault);

  const status = useDataStatus(store);

  const pages = usePages(store, Model, service, status);

  const readonlyStore: IPageableCollectionStore<TIModel, TModel> = {
    status,
    ...pages,
  };

  const editItem = useSelectedItemForCollection(store, Model, service, pages.currentPageItems);

  useTriggers(store, {
    getPage: pages.getPage,
    saveChanges: editItem.saveChanges,
  });

  return [{ ...readonlyStore, ...editItem }, store] as const;
}
