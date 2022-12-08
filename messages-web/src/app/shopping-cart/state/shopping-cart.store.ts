import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { shoppingCartService } from '../infrastructure/shopping-cart.http-service';
import { ShoppingCartState } from './shopping-cart.state';

const { computeState, action } = defineStore('shopping-cart', new ShoppingCartState());

const status = computeState((state) => state.status);

const items = computeState((state) => state.cartItems);

const getDataAsync = action('get-data', async () => {
  status.value = new DataStatus('loading');
  const response = await shoppingCartService.get();
  if (response.status === HttpStatus.Success && response.data != null) {
    throw new Error('Not Implemented!');
  } else {
    status.value = new DataStatus('error', response.message);
  }
});

export const shoppingCartStore = { status, getDataAsync, items };
