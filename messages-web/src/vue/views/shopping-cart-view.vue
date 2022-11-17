<template>
  <collection-state :state="shoppingCartStore"></collection-state>
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
