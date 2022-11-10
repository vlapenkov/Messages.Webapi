import { ICollectionStoreAdd } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionstoreAdd';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef } from 'vue';

type createFn = ICollectionStoreAdd['createItem'] | undefined;

export const createItemProvider = new Provider<createFn, ShallowRef<createFn>>(() => shallowRef());
