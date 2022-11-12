<template>
  <prime-button-edit v-if="canEdit" @click="selectItem"></prime-button-edit>
</template>

<script lang="ts">
import { useEditableChecks } from '@/vue/base/containers/state/collection/composables/editable-checks.composable';
import { useSelectItem } from '@/vue/base/containers/state/collection/composables/select-item.composable';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const { canEdit } = useEditableChecks();
    const selectItem = useSelectItem();
    return { canEdit, selectItem };
  },
});
</script>

<style scoped></style>
