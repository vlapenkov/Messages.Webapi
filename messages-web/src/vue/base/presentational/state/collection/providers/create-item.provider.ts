import { IStoreAdd } from '@/app/core/services/harlem/custom-stores/@types/IStoreAdd';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

type createFn = IStoreAdd['createItem'] | undefined;

export const createItemProvider = new ShallowProvider<createFn>(
  () => shallowRef(),
  '--provide--create-item',
);
