import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { computed } from 'vue';
import { productionsHttpService } from '../infrastructure/productions.http-service';
import { ProductionModel } from '../models/production.model';

const isProductInShoppingCart = (product: ProductionModel) =>
  computed(
    () => shoppingCartStore.items.value?.find((i) => i.productId === product.id) !== undefined,
  );

async function updateStatus(id: number, status: number) {
  const response = await productionsHttpService.updateStatus(id, status);
  if (response.statusText === 'OK') return true;
  return false;
}

export const productionsService = {
  isProductInShoppingCart,
  updateStatus,
};
