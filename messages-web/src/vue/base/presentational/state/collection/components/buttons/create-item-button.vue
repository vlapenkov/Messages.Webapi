<template>
  <prime-button-add
    v-if="canAdd"
    :disabled="disabled"
    @click="create"
    :label="label"
  ></prime-button-add>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useCreate } from '@/vue/base/presentational/state/collection/composables/create-new-item.composable';
import { useEditableChecks } from '@/vue/base/presentational/state/collection/composables/editable-checks.composable';

export default defineComponent({
  props: {
    disabled: {
      type: Boolean,
      default: false,
    },
    label: {
      type: String,
      default: 'Добавить',
    },
  },
  setup() {
    const { canAdd } = useEditableChecks();
    return { create: useCreate(), canAdd };
  },
});
</script>

<style scoped></style>
