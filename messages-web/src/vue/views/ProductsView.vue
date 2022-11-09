<template>
  <div>
    <pageable-collection-state :state="productsStore">
      <data-view-collection-pageable></data-view-collection-pageable>
    </pageable-collection-state>
  </div>
</template>

<script lang="ts">
import { productsStore } from '@/app/products/state/products.store';
import { defineComponent } from 'vue';
import DataViewCollectionPageable from '../base/containers/state/pageable-collection/DataViewCollectionPageable.vue';
import PageableCollectionState from '../base/containers/state/pageable-collection/PageableCollectionState.vue';

export default defineComponent({
  components: { PageableCollectionState, DataViewCollectionPageable },
  setup() {
    return { productsStore };
  },
});
</script>

<style scoped></style>
