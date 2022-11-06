import { ModelBase } from '@/app/core/models/base/model-base';
import { NotValidData } from '../../tools/not-valid-data';
import { selected } from '../decorators/selected-item.decorator';
import { CollectionReadonlyState } from './collection-readonly.state';

export class CollectionEditableState<
  TModel extends ModelBase,
> extends CollectionReadonlyState<TModel> {
  @selected()
  itemSelected: NotValidData<TModel> | null = null;
}
