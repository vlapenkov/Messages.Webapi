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
        <tree-view-collection></tree-view-collection>
      </template>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { defineComponent } from 'vue';
import CollectionState from '../base/containers/state/CollectionState.vue';
import DataViewCollection from '../base/containers/state/data-views/collection/DataViewCollection.vue';
import TreeViewCollection from '../base/containers/state/data-views/collection/TreeViewCollection.vue';

export default defineComponent({
  components: { CollectionState, DataViewCollection, TreeViewCollection },
  setup() {
    return {
      sectionsStore,
    };
  },
});
</script>

<style scoped></style>
