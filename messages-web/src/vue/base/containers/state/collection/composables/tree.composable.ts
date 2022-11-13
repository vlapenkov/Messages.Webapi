import { treeViewProvider } from '../providers/tree-view.provider';

export function useTree() {
  const treeview = treeViewProvider.inject();

  if (treeview.value == null) {
    throw new Error('Состояние нельзя предсятавить в виде дерева');
  }

  return treeview.value();
}
