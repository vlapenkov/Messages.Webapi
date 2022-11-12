import { computed } from 'vue';
import { createItemProvider } from '../providers/create-item.provider';
import { saveChangesProvider } from '../providers/save-changes.provider';
import { selectItemProvider } from '../providers/select-item.provider';

export function useEditableChecks() {
  const createItemRef = createItemProvider.inject();
  const selectItemRef = selectItemProvider.inject();
  const saveChangesRef = saveChangesProvider.inject();

  const canEdit = computed(() => createItemRef.value != null && saveChangesRef.value != null);
  const canAdd = computed(() => saveChangesRef.value != null && selectItemRef.value != null);
  return { canEdit, canAdd };
}
