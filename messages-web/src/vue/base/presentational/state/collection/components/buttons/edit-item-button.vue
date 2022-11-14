<template>
  <prime-button-edit v-if="canEdit" @click="selectItem"></prime-button-edit>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useEditableChecks } from '@/vue/base/presentational/state/collection/composables/editable-checks.composable';
import { useSelectItem } from '@/vue/base/presentational/state/collection/composables/select-item.composable';

export default defineComponent({
  setup() {
    const { canEdit } = useEditableChecks();
    const selectItem = useSelectItem();
    return { canEdit, selectItem };
  },
});
</script>

<style scoped></style>
