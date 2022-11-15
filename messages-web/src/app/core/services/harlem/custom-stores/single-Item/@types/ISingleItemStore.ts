import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { WritableComputedRef } from 'vue';
import { DataStatus } from '../../../tools/data-status';
import { NotValidData } from '../../../tools/not-valid-data';
import { IQueryOtions } from '../../tools/@types/IQueryOptions';

export interface ISingleItemStore<TModel extends ModelBase> {
  itemSmart: (ops?: IQueryOtions) => WritableComputedRef<TModel>;
  status: WritableComputedRef<DataStatus>;
  createItem: ((payload?: void | undefined) => void) | undefined;
  selectItem: ((payload?: void | undefined) => void) | null;
  itemSelected: WritableComputedRef<NotValidData<TModel> | null>;
  saveChanges: Action<void>;
}
