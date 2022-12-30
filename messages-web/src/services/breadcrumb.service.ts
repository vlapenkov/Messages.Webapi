import type { ITreeNode, IListNode, RouteLocationState } from '@/store/breadcrumb.store';

const treeToList = (t: ITreeNode): IListNode[] => {
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

const isObjectsEqual = (
  aObj: Record<string, unknown> = {},
  bObj: Record<string, unknown> = {},
): boolean => {
  const aKeys = Object.keys(aObj);
  const bKeys = Object.keys(bObj);
  if (aKeys.length !== bKeys.length) {
    return false;
  }

  return aKeys.every((key) => {
    const aValue = aObj[key];
    const bValue = bObj[key];
    if (typeof aValue === 'object' && typeof bValue === 'object') {
      return isObjectsEqual(
        aValue as Record<string, unknown> | undefined,
        bValue as Record<string, unknown> | undefined,
      );
    }
    return String(aValue) === String(bValue);
  });
};

/**
 * @param r1 - RouteLocation
 * @param r2 - RouteLocation
 * @returns Сравнение RouteLocation по name, params, query
 */
const isRoutesEquals = (r1: RouteLocationState, r2: RouteLocationState): boolean => {
  const isNamesEquals = r1.name === r2.name;
  const isQueriesEquals = isObjectsEqual(r1.query, r2.query);
  const isParamsEquals = isObjectsEqual(r1.params, r2.params);
  return isNamesEquals && isQueriesEquals && isParamsEquals;
};

export const breadcrumbService = {
  treeToList,
  isRoutesEquals,
};
