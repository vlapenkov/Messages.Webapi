import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { WritableComputedRef } from 'vue';
import { ICollectionHttpService } from '../../../../http/custom/collection.http-service';
import { CollectionWithItemState } from './collection-with-item.state';
import {
  createCollectionReadonlyStore,
  IReadonlyCollectionStore,
} from '../readonly/collection-readonly.store';
import { NotValidData } from '../../../tools/not-valid-data';

export interface ICollectionWithItemStore<TIModel extends IModel, TModel extends ModelBase<TIModel>>
  extends IReadonlyCollectionStore<TIModel, TModel> {
  itemSelected: WritableComputedRef<NotValidData<TModel> | null>;
}

export function createCollectionWithItemStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<CollectionWithItemState<TModel>>,
  service: ICollectionHttpService<TIModel>,
) {
  const [store, readonlyStore] = createCollectionReadonlyStore(name, Model, State, service);
  const { computeState } = store;

  const itemSelected = computeState((state) => state.itemSelected);

  const extended: ICollectionWithItemStore<TIModel, TModel> = {
    ...readonlyStore,
    itemSelected,
  } as const;

  return [store, extended] as const;
}
