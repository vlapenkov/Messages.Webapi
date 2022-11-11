<template>
  <div>
    <pageable-collection-state
      :modes="[{ label: 'Сеткой', mode: 'data-view' }]"
      :state="ordersStore"
    >
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
      <template #tree-view>
        <tree-view-collection></tree-view-collection>
      </template>
    </pageable-collection-state>
  </div>
</template>

<script lang="ts">
import { ordersStore } from '@/app/orders/state/orders.store';
import { defineComponent } from 'vue';
import PageableCollectionState from '../base/containers/state/CollectionStatePageable.vue';

export default defineComponent({
  components: { PageableCollectionState },
  setup() {
    return { ordersStore };
  },
});
</script>

<style scoped></style>
