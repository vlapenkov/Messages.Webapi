import { ModelBase } from '@/app/core/models/base/model-base';
import { IQueryOtions } from '@/app/core/services/harlem/custom-stores/tools/@types/IQueryOptions';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef, WritableComputedRef } from 'vue';

export type QueryFn = (ops?: IQueryOtions) => WritableComputedRef<ModelBase>;

export const singleItemProvider = new ShallowProvider<QueryFn | undefined>(
  () => shallowRef(),
  '--state--single-item',
);
