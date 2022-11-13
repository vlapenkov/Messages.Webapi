<template>
  <prime-button-add v-if="canAdd" @click="create" label="Добавить"></prime-button-add>
</template>

<script lang="ts">
import { useCreate } from '@/vue/base/containers/state/collection/composables/create-new-item.composable';
import { useEditableChecks } from '@/vue/base/containers/state/collection/composables/editable-checks.composable';
import { defineComponent, watchEffect } from 'vue';

export default defineComponent({
  setup() {
    const create = useCreate();
    const { canAdd } = useEditableChecks();
    watchEffect(() => {
      console.log('canAdd:', canAdd.value);
    });
    return { create, canAdd };
  },
});
</script>

<style scoped></style>
