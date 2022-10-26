import { ModelBase } from '@/app/core/models/model-base';
import { createStatus, IDataStatusObject } from './tools/data-status';

export class DataCollectionStore<TModel extends ModelBase> {
  dataDefault: TModel[] | null = null;

  dataSelected: TModel[] | null = null;

  status: IDataStatusObject = createStatus();
}
