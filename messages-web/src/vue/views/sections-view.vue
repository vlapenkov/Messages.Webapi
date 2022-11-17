<template>
  <splitter class="re-splitter">
    <splitter-panel class="pr-2" :size="30">
      <collection-state
        :modes="[{ label: 'Деревом', mode: 'tree-view' }]"
        :state="sectionsStore"
        reload-on-save
      >
        <template #data-view>
          <data-view-collection></data-view-collection>
        </template>
        <template #tree-view>
          <tree-view-collection
            v-model:selectedKeys="selectedKeys"
            selectionMode="single"
            class="reshape-tree"
          >
          </tree-view-collection>
        </template>
      </collection-state>
    </splitter-panel>
    <splitter-panel class="pl-2" :size="70">
      <products-viewer :categoryId="selectedKey" />
    </splitter-panel>
  </splitter>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { TreeSelectionKeys } from 'primevue/tree';
import { computed, defineComponent, ref } from 'vue';

export default defineComponent({
  setup() {
    const selectedKeys = ref<TreeSelectionKeys>();

    const selectedKey = computed(() => {
      const keys = selectedKeys.value;
      if (keys == null) {
        return undefined;
      }
      const sk = Object.keys(keys).find((k) => keys[k] === true);
      return sk == null ? undefined : +sk;
    });
    return {
      sectionsStore,
      selectedKeys,
      selectedKey,
    };
  },
});
</script>

<style lang="scss" scoped>
.reshape-tree {
  :deep(.p-tree) {
    margin-top: 0 !important;
    border: none;
  }
}
.re-splitter {
  border: none;
  :deep(.p-splitter-panel) {
    background-color: var(--surface-ground);
  }
}
</style>
