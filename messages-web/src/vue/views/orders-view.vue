<template>
  <div>
    <collection-state :modes="[{ label: 'Сеткой', mode: 'data-view' }]" :state="ordersStore">
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { ordersStore } from '@/app/orders/state/orders.store';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return { ordersStore };
  },
});
</script>

<style scoped></style>
