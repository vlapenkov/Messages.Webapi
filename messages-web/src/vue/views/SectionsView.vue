<template>
  <div>
    <collection-state :state="sectionsStore">
      <data-view-collection reload-on-save></data-view-collection>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { defineComponent } from 'vue';
import DataViewCollection from '../base/containers/state/DataViewCollection.vue';
import CollectionState from '../base/containers/state/CollectionState.vue';

export default defineComponent({
  setup() {
    return {
      sectionsStore,
    };
  },
  components: { CollectionState, DataViewCollection },
});
</script>

<style scoped></style>
