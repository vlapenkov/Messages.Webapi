import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IPageableCollectionHttpServie } from '../../../http/custom/pageable-collection.http-service';
import { DefaultStore, createDefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { useDataStatus } from '../tools/useDataStatus';
import { usePages } from '../tools/usePages';
import { useSelectedItemForCollection } from '../tools/useSelectedItem';
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

  return [{ ...readonlyStore, ...editItem }, store] as const;
}
