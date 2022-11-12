import { createItemProvider } from '../providers/create-item.provider';
import { itemSelectedProvider } from '../providers/item-selected.provider';
import { showDialogProvider } from '../providers/show-dialog.provider';

export function useCreate() {
  const createItem = createItemProvider.inject();
  const itemSelected = itemSelectedProvider.inject();
  const showDialog = showDialogProvider.inject();
  const create = () => {
    if (createItem.value == null || itemSelected.value === undefined) {
      throw new Error('Canot edit uneditable state!');
    }

    createItem.value();
    showDialog.value = true;
  };
  return create;
}
