<template>
  <prime-dialog
    :header="mode === 'create' ? 'Создание нового элемента' : 'Редактирование элемента'"
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
import { defineComponent } from 'vue';
import { useEditableChecks } from '@/vue/base/presentational/state/collection/composables/editable-checks.composable';
import { useSaveChanges } from '@/vue/base/presentational/state/collection/composables/save-changes.composable';
import { useSelectedData } from '@/vue/base/presentational/state/collection/composables/selected-data.composable';
import { editOrCreateModeProvider } from '@/vue/base/presentational/state/collection/providers/edit-or-create-mode.provider';

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
