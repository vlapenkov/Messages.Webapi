import type { ITreeNode, IListNode } from '@/store/breadcrumb.store';

export const treeToList = (t: ITreeNode): IListNode[] => {
  const res: IListNode[] = [];
  const nodeStack: ITreeNode[] = [t];
  const parentIdStack: (symbol | null)[] = [];
  while (nodeStack.length !== 0) {
    const node: ITreeNode | undefined = nodeStack.pop();
    if (node == null) {
      throw new Error('Что-то пошло не так при разборе дерева в список');
    }
    const nodeId = Symbol('id');
    const idToPush = parentIdStack.length === 0 ? null : parentIdStack[parentIdStack.length - 1];
    if (node.children != null) {
      node.children.forEach((x) => {
        nodeStack.push(x);
        parentIdStack.push(nodeId);
      });
      parentIdStack.push(idToPush);
    }
    res.push({
      label: node.label,
      route: node.route,
      id: nodeId,
      parentId: parentIdStack.pop(),
    } as IListNode);
  }
  return res;
};
