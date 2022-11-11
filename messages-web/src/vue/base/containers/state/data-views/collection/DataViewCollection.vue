<template>
  <loading-status-handler :status="loadingStatus">
    <data-view class="border-round" :layout="viewLayout" :value="items">
      <template #list="{ data }">
        <div class="col-12">
          <data-card class="shadow-none" :data="data"> </data-card>
        </div>
      </template>
      <template #grid="{ data }">
        <div class="col-12 md:col-6 lg:col-4 p-2">
          <data-card class="border-1 h-full" :data="data"></data-card>
        </div>
      </template>
    </data-view>
  </loading-status-handler>
</template>

<script lang="ts">
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { screenLarge } from '@/app/core/services/window/window.service';
import { computed, defineComponent } from 'vue';
import { createItemProvider } from '../../_providers/create-item.provider';
import { getItemsCollectionProvider } from '../../_providers/get-items-collection.provider';
import { itemSelectedProvider } from '../../_providers/item-selected.provider';
import { itemsCollectionProvider } from '../../_providers/items-collection.provider';
import { loadingStatusProvider } from '../../_providers/loading-status.provider';
import { reloadOnSaveProvider } from '../../_providers/reload-on-save.provider';
import { saveChangesProvider } from '../../_providers/save-changes.provider';
import { showDialogProvider } from '../../_providers/show-dialog.provider';

export default defineComponent({
  setup() {
    const reloadOnSave = reloadOnSaveProvider.inject();
    const showDialog = showDialogProvider.inject();
    const loadingStatus = loadingStatusProvider.inject().value;
    const wrappedItems = itemsCollectionProvider.inject();
    const createItem = createItemProvider.inject();
    const itemSelected = itemSelectedProvider.inject();
    const getItems = getItemsCollectionProvider.inject();
    const saveState = saveChangesProvider.inject();

    if (wrappedItems.value == null) {
      throw new Error('невозможно показать коллекцию элементов');
    }

    const items = wrappedItems.value();

    const create = () => {
      if (createItem.value == null || itemSelected === undefined) {
        throw new Error('Canot edit uneditable state!');
      }

      createItem.value();
      showDialog.value = true;
    };

    const mode = computed(() => itemSelected.value?.value?.mode);

    const selectedData = computed({
      get: () => itemSelected.value?.value?.data,
      set: (val) => {
        if (itemSelected.value?.value == null || mode.value == null || val == null) {
          return;
        }
        itemSelected.value.value = new NotValidData(val, mode.value);
      },
    });

    const saveChanges = () => {
      if (getItems.value != null && saveState.value != null) {
        saveState.value();
        showDialog.value = false;
        if (reloadOnSave.value) {
          getItems.value({ force: true });
        }
      }
    };

    const viewLayout = computed(() => (screenLarge.value ? 'grid' : 'list'));

    return {
      loadingStatus,
      items,
      create,
      showDialog,
      selectedData,
      mode,
      saveChanges,
      viewLayout,
    };
  },
});
</script>

<style scoped></style>
