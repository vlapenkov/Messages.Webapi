<template>
  <prime-dialog header="Создание нового элемента" v-model:visible="showDialog">
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
});
</script>

<style scoped></style>
