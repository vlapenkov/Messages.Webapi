import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { computed } from 'vue';
import { editOrCreateModeProvider } from '../providers/edit-or-create-mode.provider';
import { itemSelectedProvider } from '../providers/item-selected.provider';

export function useSelectedData() {
  const itemSelected = itemSelectedProvider.inject();
  const mode = editOrCreateModeProvider.inject();
  const selectedData = computed({
    get: () => itemSelected?.value?.value?.data,
    set: (val) => {
      // console.log('setting', { val, si: itemSelected.value, mode: mode.value });

      if (itemSelected.value == null || val == null || mode.value == null) {
        return;
      }
      itemSelected.value.value = new NotValidData(val, mode.value);
    },
  });

  return selectedData;
}
