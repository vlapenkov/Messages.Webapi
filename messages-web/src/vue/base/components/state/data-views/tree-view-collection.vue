<template>
  <loading-status-handler>
    <tree :value="tree"></tree>
  </loading-status-handler>
</template>

<script lang="ts">
import { treeViewProvider } from '@/vue/base/containers/state/collection/providers/tree-view.provider';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const treeview = treeViewProvider.inject();

    if (treeview.value == null) {
      throw new Error("Your state doesn't have a treeView provider");
    }

    const tree = treeview.value();

    return { tree };
  },
});
</script>

<style scoped></style>
