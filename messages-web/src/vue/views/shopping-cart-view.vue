<template>
  <div>
    <h2>Корзина</h2>
    <collection-state :modes="[{ label: 'Списком', mode: 'data-view' }]" :state="shoppingCartStore">
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {
      shoppingCartStore,
    };
  },
});
</script>

<style scoped></style>
