import { IStoreEdit } from '@/app/core/services/harlem/custom-stores/@types/IStoreEdit';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

type SelectFn = IStoreEdit['selectItem'] | undefined;

export const selectItemProvider = new ShallowProvider<SelectFn>(
  () => shallowRef(),
  '--provide--select-item',
);
