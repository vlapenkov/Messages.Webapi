import { ModelBase } from '@/app/core/models/base/model-base';
import { IQueryOtions } from '@/app/core/services/harlem/custom-stores/tools/@types/IQueryOptions';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef, WritableComputedRef } from 'vue';

export const itemsCollectionProvider = new Provider<
  (ops?: IQueryOtions) => WritableComputedRef<ModelBase[] | null>,
  ShallowRef<undefined | ((ops?: IQueryOtions) => WritableComputedRef<ModelBase[] | null>)>
>(() => shallowRef<(ops?: IQueryOtions) => WritableComputedRef<ModelBase[] | null>>());
