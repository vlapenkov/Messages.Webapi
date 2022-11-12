import { ModelBase } from '@/app/core/models/base/model-base';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef, WritableComputedRef } from 'vue';

type ItemSelected = WritableComputedRef<NotValidData<ModelBase> | null> | undefined;

export const itemSelectedProvider = new ShallowProvider<ItemSelected | undefined>(
  () => shallowRef(),
  '--provide-item-selected',
);
