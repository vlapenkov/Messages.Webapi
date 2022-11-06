<template>
  <div>
    <collection-state :state="sectionsStore">
      <template #data-view>
        <data-view-collection reload-on-save></data-view-collection>
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
import DataViewCollection from '../base/containers/state/DataViewCollection.vue';
import CollectionState from '../base/containers/state/CollectionState.vue';
import TreeViewCollection from '../base/containers/state/TreeViewCollection.vue';

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
