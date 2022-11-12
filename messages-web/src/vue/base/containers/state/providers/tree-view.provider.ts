import { IStoreTree } from '@/app/core/services/harlem/custom-stores/@types/IStoreTree';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

type TreeFn = IStoreTree['treeView'] | undefined;

export const treeViewProvider = new ShallowProvider<TreeFn>(
  () => shallowRef(),
  '--provide--tree-view',
);
