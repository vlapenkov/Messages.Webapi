import { TreeNode } from 'primevue/tree';
import { ComputedRef } from 'vue';
import { IQueryOtions } from '../../tools/@types/IQueryOptions';

export interface ICollectionStoreTree {
  treeView: (ops?: IQueryOtions) => ComputedRef<TreeNode[]>;
}
