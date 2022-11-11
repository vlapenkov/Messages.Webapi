import { ModelBase } from '@/app/core/models/base/model-base';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef, WritableComputedRef } from 'vue';

type ItemSelected = WritableComputedRef<NotValidData<ModelBase> | null> | undefined;

export const itemSelectedProvider = new Provider<
  ItemSelected,
  ShallowRef<ItemSelected | undefined>
>(() => shallowRef(), '--provide-item-selected');
