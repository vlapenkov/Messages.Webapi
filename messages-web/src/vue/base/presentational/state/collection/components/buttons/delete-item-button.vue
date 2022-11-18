<template>
  <prime-button-delete
    v-if="canDelete"
    :disabled="disabled"
    @click="deleteItem"
    label="Добавить"
  ></prime-button-delete>
</template>

<script lang="ts">
import { useDelete } from '@/app/core/handlers/http/queries/htttp-queries.wrappers';
import { defineComponent } from 'vue';
import { useEditableChecks } from '../../composables/editable-checks.composable';

export default defineComponent({
  props: {
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  setup() {
    const { canDelete } = useEditableChecks();
    const deleteItem = useDelete();
    return { canDelete, deleteItem };
  },
});
</script>

<style scoped></style>
