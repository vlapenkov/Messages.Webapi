import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { WritableComputedRef } from 'vue';
import { DataStatus } from '../../../tools/data-status';

export interface ICollectionStoreRead<TIModel extends IModel, TModel extends ModelBase<TIModel>> {
  readonly status: WritableComputedRef<DataStatus>;
  readonly items: (ops?: { force: boolean }) => WritableComputedRef<TModel[] | null>;
}
