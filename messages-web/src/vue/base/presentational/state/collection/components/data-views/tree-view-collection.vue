<template>
  <loading-status-handler>
    <tree
      class="mt-2 label-full"
      :selectionMode="selectionMode"
      v-model:selectionKeys="selectedKey"
      :value="tree"
    >
      <template #default="{ node }">
        <model-provider :data="node.data">
          <hover-tag
            #default="{ hover }"
            style="min-height: 50px"
            class="flex border-round justify-content-between flex-row align-items-center gap-3 ttree-node-hover"
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
      <template v-for="slotName in slotNames" #[slotName]="{ node }" :key="slotName">
        <slot :name="slotName" :node="node"> </slot>
      </template>
    </tree>
  </loading-status-handler>
</template>

<script lang="ts">
import { TreeSelectionKeys } from 'primevue/tree';
import { computed, defineComponent, PropType } from 'vue';
import { useTree } from '../../composables/tree.composable';

type TreeSelectionModeType = 'single' | 'multiple' | 'checkbox' | undefined;

export default defineComponent({
  props: {
    selectionMode: {
      type: String as PropType<TreeSelectionModeType>,
      default: undefined,
    },
    selectedKeys: {
      type: Object as PropType<TreeSelectionKeys>,
    },
  },
  emits: {
    'update:selectedKeys': (_: TreeSelectionKeys | undefined) => true,
  },
  setup(props, { slots, emit }) {
    const selectedKey = computed({
      get: () => props.selectedKeys,
      set: (val) => emit('update:selectedKeys', val),
    });
    return { tree: useTree(), slotNames: Object.keys(slots), selectedKey };
  },
});
</script>

<style lang="scss" scoped>
.label-full {
  :deep(.p-treenode-label) {
    width: 100%;
    margin-right: 0.25rem;
  }
  :deep(.p-treenode-content) {
    padding: 0.25rem 0 !important;
  }
}

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

.tree-node-hover {
  transition: background-color 0.2s ease-in-out;
  &:hover {
    background-color: var(--surface-ground);
  }
}
</style>
