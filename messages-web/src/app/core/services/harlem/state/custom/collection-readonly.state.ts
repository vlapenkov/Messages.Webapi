import { ModelBase } from '@/app/core/models/base/model-base';
import { DataStatus } from '../../tools/data-status';
import { StateBase } from '../base/state-base';
import { collection } from '../decorators/collection.decorator';
import { dataStatus } from '../decorators/data-status.decorator';

export class CollectionReadonlyState<TModel extends ModelBase> extends StateBase {
  @collection
  items: TModel[] | null = null;

  @dataStatus
  status = new DataStatus();
}
