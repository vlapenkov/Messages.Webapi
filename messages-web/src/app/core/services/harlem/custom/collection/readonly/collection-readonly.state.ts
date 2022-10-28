import { ModelBase } from '@/app/core/models/base/model-base';
import { DataStatus } from '../../../tools/data-status';

export class CollectionReadonlyState<TModel extends ModelBase> {
  items: TModel[] | null = null;

  status = new DataStatus();
}
