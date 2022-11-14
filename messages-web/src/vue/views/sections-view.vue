<template>
  <div>
    <collection-state
      :modes="[
        { label: 'Деревом', mode: 'tree-view' },
        { label: 'Сеткой', mode: 'data-view' },
      ]"
      :state="sectionsStore"
      reload-on-save
    >
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
      <template #tree-view>
        <splitter class="mt-2">
          <splitter-panel>
            <tree-view-collection class="reshape-tree"></tree-view-collection>
          </splitter-panel>
          <splitter-panel>
            <router-view></router-view>
          </splitter-panel>
        </splitter>
      </template>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {
      sectionsStore,
    };
  },
});
</script>

<style lang="scss" scoped>
.reshape-tree {
  box-sizing: border-box;
  :deep(.p-tree) {
    margin-top: 0 !important;
    border: none;
  }
}
</style>
