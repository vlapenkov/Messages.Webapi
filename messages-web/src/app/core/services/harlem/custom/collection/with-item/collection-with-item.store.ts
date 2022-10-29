import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionHttpService } from '../../../../http/custom/collection.http-service';
import { CollectionWithItemState } from './collection-with-item.state';
import { createCollectionReadonlyStore } from '../readonly/collection-readonly.store';

export function createCollectionWithItemStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<CollectionWithItemState<TModel>>,
  service: ICollectionHttpService<TIModel>,
) {
  const [store, { items, itemsAsync, loadingStatus }] = createCollectionReadonlyStore(
    name,
    Model,
    State,
    service,
  );
  const { computeState } = store;

  const itemSelected = computeState((state) => state.itemSelected);

  const extended = { loadingStatus, itemsAsync, items, itemSelected } as const;

  return [store, extended] as const;
}
