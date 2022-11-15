import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionHttpService } from '../../../http/custom/collection.http-service';
import { createDefaultStore, DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { ICollectionStoreRead } from './@types/ICollectionStoreRead';
import { useDataStatus } from '../tools/useDataStatus';
import { useCollectionItems } from '../tools/useCollectionItems';
import { useSelectedItemForCollection } from '../tools/useSelectedItem';
import { useTreeView } from '../tools/useTreeView';
import { CollectionStore } from './@types/CollectionStore';

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

  const status = useDataStatus(store);

  const { itemsDumb, itemsSmart, getDataAsync } = useCollectionItems(store, Model, service, status);

  const readolnlyCollectionStore: ICollectionStoreRead<TIModel, TModel> = {
    status,
    items: itemsSmart,
    getDataAsync,
  };

  const selected = useSelectedItemForCollection(store, Model, service, itemsDumb);

  const treeView = useTreeView(store, (o) => itemsSmart(o));

  const editableCollectionStore: CollectionStore<TIModel, TModel> = {
    ...readolnlyCollectionStore,
    ...selected,
    treeView,
  };

  return [editableCollectionStore, store] as const;
}
