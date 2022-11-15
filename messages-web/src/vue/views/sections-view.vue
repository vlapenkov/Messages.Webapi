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
      <router-view v-slot="{ Component }">
        <transition-fade>
          <component :is="Component"></component>
        </transition-fade>
      </router-view>
    </splitter-panel>
  </splitter>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { TreeSelectionKeys } from 'primevue/tree';
import { defineComponent, ref, watch } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  setup() {
    const selectedKeys = ref<TreeSelectionKeys>();
    const router = useRouter();
    watch(selectedKeys, (val) => {
      if (val == null) {
        return;
      }
      const selectedKey = Object.keys(val).find((k) => val[k] === true);
      if (selectedKey != null) {
        router.push({ name: 'section-products', params: { sectionId: selectedKey } });
      }
    });
    return {
      sectionsStore,
      selectedKeys,
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
