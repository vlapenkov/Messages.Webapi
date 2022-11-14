import { modelProvider } from '../providers/model-provider';
import { selectItemProvider } from '../providers/select-item.provider';
import { showDialogProvider } from '../providers/show-dialog.provider';

export function useSelectItem() {
  const itemRef = modelProvider.inject();
  const selectItemRef = selectItemProvider.inject();
  const showDialog = showDialogProvider.inject();
  const selectItem = () => {
    if (selectItemRef.value == null || itemRef.value == null) {
      throw new Error('Элемент нельзя редактировать');
    }

    selectItemRef.value(itemRef.value.key);
    showDialog.value = true;
  };
  return selectItem;
}
