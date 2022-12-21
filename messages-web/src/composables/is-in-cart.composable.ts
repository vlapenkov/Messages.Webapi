import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { computed, onMounted } from 'vue';

export function useIsInCart(productId: number) {
  const { items, status, getDataAsync } = shoppingCartStore;
  onMounted(() => {
    if (status.value.status === 'initial') {
      getDataAsync();
    }
  });
  return computed(() => items.value?.find((x) => x.productId === productId) != null);
}
