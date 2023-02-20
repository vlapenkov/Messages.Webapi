import { IStoreDelete } from '@/app/core/services/harlem/custom-stores/@types/IStoreDelete';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

type DeleteFn = IStoreDelete['deleteItem'];

export const deleteItemProvider = new ShallowProvider<DeleteFn | undefined>(() => shallowRef());
