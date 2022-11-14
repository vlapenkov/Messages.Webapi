<template>
  <loading-status-handler>
    <tree class="mt-2 label-full" :value="tree">
      <template #default="{ node }">
        <model-provider :data="node.data">
          <hover-tag
            #default="{ hover }"
            style="min-height: 50px"
            class="flex w-full border-round justify-content-between p-2 flex-row align-items-center gap-3 tree-node-hover"
          >
            <span>{{ node.label }}</span>
            <transition
              mode="out-in"
              enter-active-class="fadeinleft animation-duration-100"
              leave-active-class="fadeoutleft animation-duration-100"
            >
              <div v-if="hover" class="flex flex-row gap-2">
                <edit-item-button class="icon-small" />
              </div>
            </transition>
          </hover-tag>
        </model-provider>
      </template>
    </tree>
  </loading-status-handler>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useTree } from '../../composables/tree.composable';

export default defineComponent({
  setup() {
    return { tree: useTree() };
  },
});
</script>

<style lang="scss" scoped>
$size: 0;
$padding-and-font: 1rem;
.icon-small {
  height: $size !important;
  width: $size;
  border-radius: $padding-and-font * 2;
  padding: $padding-and-font;
  :deep(.p-button-icon) {
    font-size: $padding-and-font;
  }
}
.label-full {
  :deep(.p-treenode-label) {
    width: 100%;
  }
  :deep(.p-treenode-content) {
    padding: 0.25rem 0 !important;
  }
}

.tree-node-hover {
  transition: background-color 0.2s ease-in-out;
  &:hover {
    background-color: var(--surface-ground);
  }
}
</style>
