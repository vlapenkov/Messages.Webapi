import { ModelBase } from '@/app/core/models/base/model-base';
import { NotValidData } from '../../tools/not-valid-data';
import { DataStatus } from '../../tools/data-status';

export class CollectionState<TModel extends ModelBase> {
  items: TModel[] | null = null;

  itemSelected: NotValidData<TModel> | null = null;

  status = new DataStatus();
}
