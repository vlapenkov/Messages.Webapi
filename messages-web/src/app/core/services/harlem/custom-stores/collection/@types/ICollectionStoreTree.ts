import { TreeNode } from 'primevue/tree';
import { ComputedRef } from 'vue';

export interface ICollectionStoreTree {
  treeView: (ops?: { force: boolean }) => ComputedRef<TreeNode[]>;
}
