<template>
  <app-page title="Корзина">
    <div class="flex flex-column">
      <card class="shopping-cart-inner-card">
        <template #content>
          <shopping-cart-item v-for="item in items" :key="item.productId" :item="item" />
          <div class="flex flex-row justify-content-between align-items-center mt-1">
            <div class="p-component text-lg font-semibold">Общая стоимость: {{ sum }} ₽</div>
            <prime-button @click="createNewOrder" label="Оформить заказ"
              :disabled="createNewOrderDisabled"></prime-button>
          </div>
        </template>
      </card>
    </div>
  </app-page>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, ref } from 'vue';
import { ordersHttpService } from '@/app/orders/infrastructure/order.http-service';
import { useRouter } from 'vue-router';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { reloadOnSaveProvider } from '../base/presentational/state/collection/providers/reload-on-save.provider';

export default defineComponent({
  setup() {
    const router = useRouter();
    reloadOnSaveProvider.provide(ref(true));
    const { items, getDataAsync } = shoppingCartStore;
    onMounted(() => {
      getDataAsync();
    });

    const createNewOrder = async () => {
      const response = await ordersHttpService.postOrder(undefined);
      console.log(response);
      router.push({ name: 'orders' });
    };

    const createNewOrderDisabled = computed(() => (items.value ?? []).length === 0);
    const sum = computed(() =>
      (items.value ?? []).map((i) => i.price * i.quantity).reduce((acc, curr) => acc + curr, 0),
    );
    return {
      sum,
      items,
      createNewOrderDisabled,
      createNewOrder,
    };
  },
});
</script>

<style lang="scss" scoped>
.shopping-cart-inner-card {
  :deep(.p-card-content) {
    padding-top: 0;
    padding-bottom: 0;
  }
}
</style>
