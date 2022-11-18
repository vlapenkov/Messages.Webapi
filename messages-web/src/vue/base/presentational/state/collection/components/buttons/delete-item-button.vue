<template>
  <prime-button-delete
    v-if="canDelete"
    :disabled="disabled"
    @click="deleteItem"
    label="Удалить"
  ></prime-button-delete>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useDelete } from '../../composables/delete-item.composable';
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
