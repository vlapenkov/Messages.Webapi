<template>
  <div>
    <pageable-collection-state :state="productsStore"> </pageable-collection-state>
  </div>
</template>

<script lang="ts">
import { productsStore } from '@/app/products/state/products.store';
import { defineComponent } from 'vue';
import PageableCollectionState from '../base/containers/state/CollectionStatePageable.vue';

export default defineComponent({
  components: { PageableCollectionState },
  setup() {
    return { productsStore };
  },
});
</script>

<style scoped></style>
