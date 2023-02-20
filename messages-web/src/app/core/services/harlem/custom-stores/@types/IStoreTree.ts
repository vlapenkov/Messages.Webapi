import { TreeNode } from 'primevue/tree';
import { ComputedRef } from 'vue';
import { IQueryOtions } from '../composables/@types/IQueryOptions';

export interface IStoreTree {
  treeView: (ops?: IQueryOtions) => ComputedRef<TreeNode[]>;
}
