<template>
  <loading-status-handler :status="loadingStatus">
    <data-view :value="items">
      <template #list="{ data }">
        <div class="col-12">
          <data-card class="shadow-none" :data="data"> </data-card>
        </div>
      </template>
      <template #header>
        <div v-if="isEditable" class="flex justify-content-end">
          <prime-button-add @click="create" label="Добавить"></prime-button-add>
        </div>
      </template>
    </data-view>
  </loading-status-handler>
  <prime-dialog header="Создание нового элемента" v-model:visible="showDialog">
    <custom-form class="shadow-none" v-model:data="selectedData">
      <template #footer>
        <div v-if="isEditable && mode != null" class="flex justify-content-end">
          <prime-button-add
            @click="saveChanges"
            v-if="mode === 'create'"
            label="Добавить"
          ></prime-button-add>
          <prime-button-save v-else label="Сохранить"></prime-button-save>
        </div>
      </template>
    </custom-form>
  </prime-dialog>
</template>

<script lang="ts">
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { computed, defineComponent, ref } from 'vue';
import Dialog from 'primevue/dialog';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { injectCollectionState } from './CollectionState.vue';

export default defineComponent({
  components: { PrimeDialog: Dialog },

  setup() {
    const currentState = injectCollectionState();

    const loadingStatus = computed<DataStatus | undefined>(
      () => currentState.value?.loadingStatus.value,
    );
    const items = currentState.value?.itemsAsync();
    const isEditable = computed(
      () => currentState.value?.selectItem != null && currentState.value.saveChanges != null,
    );

    const showDialog = ref(false);

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
      }
    };

    return {
      loadingStatus,
      items,
      isEditable,
      create,
      showDialog,
      selectedData,
      mode,
      saveChanges,
    };
  },
});
</script>

<style scoped></style>
