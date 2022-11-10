import { TreeNode } from 'primevue/tree';
import { ComputedRef } from 'vue';
import { IQueryOtions } from '../tools/@types/IQueryOptions';

export interface IStoreTree {
  treeView: (ops?: IQueryOtions) => ComputedRef<TreeNode[]>;
}
