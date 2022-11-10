import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { WritableComputedRef } from 'vue';
import { DataStatus } from '../../../tools/data-status';
import { IQueryOtions } from '../../tools/@types/IQueryOptions';

export interface ICollectionStoreRead<TIModel extends IModel, TModel extends ModelBase<TIModel>> {
  readonly status: WritableComputedRef<DataStatus>;
  readonly items: (ops?: IQueryOtions) => WritableComputedRef<TModel[] | null>;
  getDataAsync: Action<IQueryOtions | undefined, TModel[] | null>;
}
