<template>
  <div>
    <collection-state-pageable
      :modes="[{ label: 'Сеткой', mode: 'data-view' }]"
      :state="productsStore"
    >
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
      <template #tree-view>
        <tree-view-collection></tree-view-collection>
      </template>
    </collection-state-pageable>
  </div>
</template>

<script lang="ts">
import { productsStore } from '@/app/products/state/products.store';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return { productsStore };
  },
});
</script>

<style scoped></style>
