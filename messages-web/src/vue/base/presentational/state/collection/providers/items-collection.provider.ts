import { ModelBase } from '@/app/core/models/base/model-base';
import { IQueryOtions } from '@/app/core/services/harlem/custom-stores/composables/@types/IQueryOptions';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef, WritableComputedRef } from 'vue';

export const itemsCollectionProvider = new ShallowProvider<
  ((ops?: IQueryOtions) => WritableComputedRef<ModelBase[] | null>) | undefined
>(() => shallowRef(), '--provide--items-collection');
