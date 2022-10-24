import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/model-base';
import { createStatus, IDataStatusObject } from './tools/data-status';

export class DataStore<TModel extends ModelBase<IModel>> {
  dataDefault: TModel | null = null;

  dataSelected: TModel | null = null;

  status: IDataStatusObject = createStatus();
}
