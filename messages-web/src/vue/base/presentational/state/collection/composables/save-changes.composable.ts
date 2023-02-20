import { computed } from 'vue';
import { refreshProvider } from '../providers/refresh.provider';
import { reloadOnSaveProvider } from '../providers/reload-on-save.provider';
import { saveChangesProvider } from '../providers/save-changes.provider';
import { showDialogProvider } from '../providers/show-dialog.provider';

export function useSaveChanges() {
  const saveChangesRef = saveChangesProvider.inject();
  const showDialog = showDialogProvider.inject();
  const reloadDataRef = refreshProvider.inject();
  const reloadOnSave = reloadOnSaveProvider.inject();

  const throwError = (message: string) => () => {
    throw new Error(message);
  };
  const saveChangesFn = computed(() =>
    saveChangesRef.value != null
      ? saveChangesRef.value
      : throwError('Не получена зависимость saveChanges'),
  );
  const reloadDataFn = computed(() =>
    reloadDataRef.value != null
      ? reloadDataRef.value
      : throwError('Не получена функция перезагрузки данных'),
  );

  const saveChanges = async () => {
    await saveChangesFn.value();
    showDialog.value = false;
    if (reloadOnSave.value) {
      reloadDataFn.value();
    }
  };

  return { saveChanges, showDialog };
}
