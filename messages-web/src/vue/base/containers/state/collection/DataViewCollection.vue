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
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { computed, defineComponent } from 'vue';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { screenLarge } from '@/app/core/services/window/window.service';
import {
  collectionStateProvider,
  reloadOnSaveProvider,
  showDialogProvider,
} from './CollectionState.vue';

export default defineComponent({
  setup() {
    const currentState = collectionStateProvider.inject();
    const reloadOnSave = reloadOnSaveProvider.inject();
    const showDialog = showDialogProvider.inject();

    const loadingStatus = computed<DataStatus | undefined>(() => currentState.value?.status.value);
    const items = currentState.value?.items();

    const create = () => {
      if (currentState.value == null) {
        return;
      }
      const { createItem, itemSelected } = currentState.value;
      if (createItem == null || itemSelected === undefined) {
        throw new Error('Canot edit uneditable state!');
      }

      createItem();
      showDialog.value = true;
    };

    const mode = computed(() => currentState.value?.itemSelected?.value?.mode);

    const selectedData = computed({
      get: () => currentState.value?.itemSelected?.value?.data,
      set: (val) => {
        if (currentState.value?.itemSelected?.value == null || val == null) {
          return;
        }
        currentState.value.itemSelected.value = new NotValidData(
          val,
          currentState.value.itemSelected.value.mode,
        );
      },
    });

    const saveChanges = () => {
      if (currentState.value != null && currentState.value.saveChanges != null) {
        currentState.value.saveChanges();
        showDialog.value = false;
        if (reloadOnSave.value) {
          currentState.value.getDataAsync({ force: true });
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
