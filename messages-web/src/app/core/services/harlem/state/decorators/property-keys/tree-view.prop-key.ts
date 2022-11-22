import { ModelBase } from '@/app/core/models/base/model-base';
import { TreeNode } from 'primevue/tree';
import { StateBase } from '../../base/state-base';

export const treeViewPropKeyFor = (key: string) => Symbol.for(`--state--tree-view-${key}`);

export const getTreeViewTransform = <T extends StateBase, TModel extends ModelBase>(
  target: T,
  key: string,
) =>
  target[treeViewPropKeyFor(key) as unknown as keyof T] as unknown as (
    arg: TModel | TModel[],
  ) => TreeNode[];
