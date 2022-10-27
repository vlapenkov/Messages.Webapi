import { ModelBase } from '@/app/core/models/base/model-base';
import { DataStatus } from '../../tools/data-status';

export class SingleState<TModel extends ModelBase> {
  dataDefault: TModel | null = null;

  dataSelected: TModel | null = null;

  status = new DataStatus();
}
