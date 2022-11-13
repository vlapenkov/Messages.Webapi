<template>
  <prime-dialog
    header="Создание нового элемента"
    :breakpoints="{ '900px': '75vw', '720px': '90vw' }"
    :style="{ 'width': '50vw', 'max-width': '800px' }"
    class="re-padding"
    :draggable="false"
    modal
    v-model:visible="showDialog"
  >
    <custom-form class="shadow-none" v-model:data="selectedData">
      <template #footer>
        <div v-if="(canEdit || canAdd) && mode != null" class="flex justify-content-end">
          <prime-button-add
            @click="saveChanges"
            v-if="mode === 'create'"
            label="Добавить"
          ></prime-button-add>
          <prime-button-save @click="saveChanges" v-else label="Сохранить"></prime-button-save>
        </div>
      </template>
    </custom-form>
  </prime-dialog>
</template>

<script lang="ts">
import { PrimeDialog } from '@/tools/prime-vue-components';
import { useEditableChecks } from '@/vue/base/containers/state/collection/composables/editable-checks.composable';
import { useSaveChanges } from '@/vue/base/containers/state/collection/composables/save-changes.composable';
import { useSelectedData } from '@/vue/base/containers/state/collection/composables/selected-data.composable';
import { editOrCreateModeProvider } from '@/vue/base/containers/state/collection/providers/edit-or-create-mode.provider';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const mode = editOrCreateModeProvider.inject();
    const selectedData = useSelectedData();
    return { ...useSaveChanges(), ...useEditableChecks(), mode, selectedData };
  },
  components: { PrimeDialog },
});
</script>

<style lang="scss">
.re-padding {
  .p-dialog-content {
    padding: 1rem;
    .p-card-body {
      padding: 0;
    }
    .p-card-content {
      padding-bottom: 0;
    }
  }
  .p-dialog-header {
    padding-bottom: 0;
  }
}
</style>
