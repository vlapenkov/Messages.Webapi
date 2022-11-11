import { IStoreTree } from '@/app/core/services/harlem/custom-stores/@types/IStoreTree';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef } from 'vue';

type TreeFn = IStoreTree['treeView'] | undefined;

export const treeViewProvider = new Provider<TreeFn, ShallowRef<TreeFn>>(
  () => shallowRef(),
  '--provide--tree-view',
);
