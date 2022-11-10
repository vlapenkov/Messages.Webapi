import { IStoreAdd } from '@/app/core/services/harlem/custom-stores/@types/IStoreAdd';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef } from 'vue';

type createFn = IStoreAdd['createItem'] | undefined;

export const createItemProvider = new Provider<createFn, ShallowRef<createFn>>(() => shallowRef());
