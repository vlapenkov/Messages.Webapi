import { ModelBase } from '@/app/core/models/model-base';
import { DataStatus } from '../base/tools/data-status';

export class CollectionStore<TModel extends ModelBase> {
  dataDefault: TModel[] | null = null;

  dataSelected: TModel[] | null = null;

  status = new DataStatus();
}
