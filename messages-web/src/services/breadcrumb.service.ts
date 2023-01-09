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

const hasOrMoreObjectKeys = (
  aObj: Record<string, unknown> = {},
  bObj: Record<string, unknown> = {},
): boolean => {
  const aKeys = Object.keys(aObj);
  const bKeys = Object.keys(bObj);
  return aKeys.every((x) => bKeys.some((y) => x === y));
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
 * @param treeRoute - tree route
 * @param location - current location
 * @returns Сравнение RouteLocation по name, params, query
 */
const isRoutesEquals = (treeRoute: RouteLocationState, location: RouteLocationState): boolean => {
  const isNamesEquals = treeRoute.name === location.name;
  const isQueriesEquals = hasOrMoreObjectKeys(treeRoute.query, location.query);
  const isParamsEquals = hasOrMoreObjectKeys(treeRoute.params, location.params);
  return isNamesEquals && isQueriesEquals && isParamsEquals;
};

export const breadcrumbService = {
  treeToList,
  isObjectsEqual,
  isRoutesEquals,
  hasOrMoreObjectKeys,
};
