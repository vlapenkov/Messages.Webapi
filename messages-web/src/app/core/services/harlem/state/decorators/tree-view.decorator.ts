import { ModelBase } from '@/app/core/models/base/model-base';
import { TreeNode } from 'primevue/tree';
import { StateBase } from '../base/state-base';
import { treeViewPropKeyFor } from './property-keys/tree-view.prop-key';

export const treeView =
  <TModel extends ModelBase>(
    toTree: ((model: TModel[]) => TreeNode[]) | ((model: TModel) => TreeNode[]),
  ) =>
  <TState extends StateBase>(target: TState, key: string) => {
    Object.defineProperty(target, treeViewPropKeyFor(key), {
      get: () => toTree,
    });
  };
