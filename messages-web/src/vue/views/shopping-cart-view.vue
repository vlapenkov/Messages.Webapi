<template>
  <div>
    <collection-state
      class="imaged"
      :modes="[{ label: 'Списком', mode: 'data-view' }]"
      :state="shoppingCartStore"
    >
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
    </collection-state>
    <div class="flex flex-row justify-content-end mt-3">
      <prime-button-save @click="create" label="Создать заказ" />
    </div>
  </div>
</template>

<script lang="ts">
import { createOrder } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  setup() {
    const router = useRouter();
    const create = async () => {
      await createOrder();
      router.push({ name: 'orders' });
    };
    return {
      shoppingCartStore,
      create,
    };
  },
});
</script>

<style scoped></style>
