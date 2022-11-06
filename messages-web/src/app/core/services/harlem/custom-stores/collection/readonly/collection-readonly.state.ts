import { ModelBase } from '@/app/core/models/base/model-base';
import { DataStatus } from '../../../tools/data-status';
import { StateBase } from '../../../state/base/state-base';

export class CollectionReadonlyState<TModel extends ModelBase> extends StateBase {
  items: TModel[] | null = null;

  status = new DataStatus();
}
