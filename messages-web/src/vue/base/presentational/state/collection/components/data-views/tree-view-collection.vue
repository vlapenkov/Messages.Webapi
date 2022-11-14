<template>
  <loading-status-handler>
    <tree class="mt-2 label-full" :value="tree">
      <template v-for="slotName in slotNames" #[slotName]="{ node }" :key="slotName">
        <slot :name="slotName" :node="node"></slot>
      </template>
    </tree>
  </loading-status-handler>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useTree } from '../../composables/tree.composable';

export default defineComponent({
  setup(_, { slots }) {
    return { tree: useTree(), slotNames: Object.keys(slots) };
  },
});
</script>

<style lang="scss" scoped>
.label-full {
  :deep(.p-treenode-label) {
    width: 100%;
  }
  :deep(.p-treenode-content) {
    padding: 0.25rem 0 !important;
  }
}
</style>
