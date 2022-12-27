import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { isAuthenticated } from '@/store/user.store';
import { computed, onMounted } from 'vue';

export function useShoppingCartItems() {
  const { items, status, getDataAsync } = shoppingCartStore;
  onMounted(() => {
    if (status.value.status === 'initial' && isAuthenticated.value) {
      getDataAsync();
    }
  });
  return items;
}

export function useIsInCart(productId: number) {
  const items = useShoppingCartItems();
  return computed(() => items.value?.find((x) => x.productId === productId) != null);
}

export function useCartTotalQuantity() {
  useShoppingCartItems();
  return shoppingCartStore.totalQuantity;
}
