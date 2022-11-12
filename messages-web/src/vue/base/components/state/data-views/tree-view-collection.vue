<template>
  <tree :value="tree"></tree>
</template>

<script lang="ts">
import { itemSelectedProvider } from '@/vue/base/containers/state/collection/providers/item-selected.provider';
import { loadingStatusProvider } from '@/vue/base/containers/state/collection/providers/loading-status.provider';
import { saveChangesProvider } from '@/vue/base/containers/state/collection/providers/save-changes.provider';
import { selectItemProvider } from '@/vue/base/containers/state/collection/providers/select-item.provider';
import { treeViewProvider } from '@/vue/base/containers/state/collection/providers/tree-view.provider';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const loadingStatus = loadingStatusProvider.inject();
    const saveState = saveChangesProvider.inject();
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    const itemSelected = itemSelectedProvider.inject();
    const selectItem = selectItemProvider.inject();
    const treeview = treeViewProvider.inject();

    const isEditable = computed(() => selectItem.value != null && saveState.value != null);

    if (treeview.value == null) {
      throw new Error("Your state doesn't have a treeView provider");
    }

    const tree = treeview.value();

    return { loadingStatus, isEditable, tree };
  },
});
</script>

<style scoped></style>
