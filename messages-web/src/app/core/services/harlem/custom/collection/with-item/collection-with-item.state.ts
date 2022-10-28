import { ModelBase } from '@/app/core/models/base/model-base';
import { NotValidData } from '../../../tools/not-valid-data';
import { CollectionReadonlyState } from '../readonly/collection-readonly.state';

export class CollectionWithItemState<
  TModel extends ModelBase,
> extends CollectionReadonlyState<TModel> {
  itemSelected: NotValidData<TModel> | null = null;
}
