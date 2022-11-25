import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { IAddToShoppingCartRequest } from '../@types/IAddToShoppingCartRequest';
import { IShoppingCartModel } from '../@types/IShoppingCartModel';

const [shoppingCartService, { definePost, defineDelete }] =
  defineCollectionService<IShoppingCartModel>({
    url: 'api/ShoppingCart',
  });

export const addToCart = definePost<void, IAddToShoppingCartRequest>();

shoppingCartService.del = defineDelete((model) => ({
  url: `/${model.productId}`,
  bodyOrParams: {},
}));

export const createOrder = definePost<void, void>();

export { shoppingCartService };
