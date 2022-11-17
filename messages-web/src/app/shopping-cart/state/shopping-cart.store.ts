import { defineCollectionStore } from '@/app/core/services/harlem/custom-stores/collection/collection.store';
import { shoppingCartService } from '../infrastructure/shopping-cart.http-service';
import { ShoppingCartModel } from '../models/shopping-cart.model';
import { ShoppingCartState } from './shopping-cart.state';

export const [shoppingCartStore] = defineCollectionStore(
  'shopping-cart',
  ShoppingCartModel,
  ShoppingCartState,
  shoppingCartService,
);
