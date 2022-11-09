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
import DataViewCollection from '../base/containers/state/collection/DataViewCollection.vue';
import CollectionState from '../base/containers/state/collection/CollectionState.vue';
import TreeViewCollection from '../base/containers/state/collection/TreeViewCollection.vue';

export default defineComponent({
  setup() {
    return {
      sectionsStore,
    };
  },
  components: { CollectionState, DataViewCollection, TreeViewCollection },
});
</script>

<style scoped></style>
