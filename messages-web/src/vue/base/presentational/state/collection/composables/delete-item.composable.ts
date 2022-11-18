import { deleteItemProvider } from '../providers/delete-item.provider';
import { modelProvider } from '../providers/model-provider';

export function useDelete() {
  const deleteItem = deleteItemProvider.inject();
  const currentItem = modelProvider.inject();
  return async () => {
    if (deleteItem.value == null || currentItem.value === undefined) {
      throw new Error('Canot delete undeletable Item!');
    }
    await deleteItem.value(currentItem.value.key);
  };
}
