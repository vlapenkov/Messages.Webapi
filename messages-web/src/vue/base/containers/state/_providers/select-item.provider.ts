import { IStoreEdit } from '@/app/core/services/harlem/custom-stores/@types/IStoreEdit';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef } from 'vue';

type SelectFn = IStoreEdit['selectItem'] | undefined;

export const selectItemProvider = new Provider<SelectFn, ShallowRef<SelectFn>>(
  () => shallowRef(),
  '--provide--select-item',
);
