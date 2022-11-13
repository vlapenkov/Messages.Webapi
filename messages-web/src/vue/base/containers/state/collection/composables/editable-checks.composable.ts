import { computed } from 'vue';
import { createItemProvider } from '../providers/create-item.provider';
import { saveChangesProvider } from '../providers/save-changes.provider';
import { selectItemProvider } from '../providers/select-item.provider';

export function useEditableChecks() {
  const createItemRef = createItemProvider.inject();
  const selectItemRef = selectItemProvider.inject();
  const saveChangesRef = saveChangesProvider.inject();

  const canEdit = computed(() => selectItemRef.value != null && saveChangesRef.value != null);
  const canAdd = computed(() => createItemRef.value != null && saveChangesRef.value != null);
  const canAddOrEdit = computed(() => canAdd.value || canEdit.value);
  // watchEffect(() => {
  //   console.group('Edit check values');
  //   console.log('select-item', selectItemRef.value != null);
  //   console.log('create-item', createItemRef.value != null);
  //   console.log('save-changes', saveChangesRef.value != null);

  //   console.log('CanAdd', canAdd.value);
  //   console.log('CanEdit', canEdit.value);
  //   console.groupEnd();
  // });
  return { canEdit, canAdd, canAddOrEdit };
}
