import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IAddToShoppingCartRequest } from '../@types/IAddToShoppingCartRequest';
import { shoppingCartHttpService } from '../infrastructure/shopping-cart.http-service';
import { ShoppingCartModel } from '../models/shopping-cart.model';
import { ShoppingCartState } from './shopping-cart.state';

const { computeState, action, onActionSuccess } = defineStore(
  'shopping-cart',
  new ShoppingCartState(),
);

const status = computeState((state) => state.status);

const items = computeState((state) => state.cartItems);

const getDataAsync = action('get-data', async () => {
  status.value = new DataStatus('loading');
  const response = await shoppingCartHttpService.get();
  if (response.status === HttpStatus.Success && response.data != null) {
    const newItems = response.data.map((i) => {
      const model = new ShoppingCartModel();
      const ps = model.fromResponse(i);
      if (ps) {
        return model;
      }
      throw new Error('Ошибка преобразования данных');
    });
    items.value = newItems;
    status.value = new DataStatus('loaded');
  } else {
    status.value = new DataStatus('error', response.message);
  }
});

const addToCartActionName = 'add-to-cart';

const addToCart = action<IAddToShoppingCartRequest>(addToCartActionName, async (payload) => {
  const response = await shoppingCartHttpService.addToCart(payload);
  if (response.status !== HttpStatus.Success) {
    throw new Error('Failed to update shopping cart');
  }
});

onActionSuccess(addToCartActionName, () => {
  getDataAsync();
});

export const shoppingCartStore = { status, getDataAsync, items, addToCart };
