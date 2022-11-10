import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { WritableComputedRef } from 'vue';
import { NotValidData } from '../../tools/not-valid-data';

export interface ISelectedItem<TIModel extends IModel, TModel extends ModelBase<TIModel>> {
  itemSelected: WritableComputedRef<NotValidData<TModel> | null>;
}
