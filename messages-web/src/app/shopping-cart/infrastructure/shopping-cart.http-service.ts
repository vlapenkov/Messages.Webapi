import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { IAddToShoppingCartRequest } from '../@types/IAddToShoppingCartRequest';
import { IShoppingCartModel } from '../@types/IShoppingCartModel';

const [shoppingCartService, { definePost, defineDelete }] =
  defineCollectionService<IShoppingCartModel>({
    url: 'api/v1/ShoppingCart',
  });

export const addToCard = definePost<void, IAddToShoppingCartRequest>();

shoppingCartService.del = defineDelete((model) => ({
  url: `/${model.productId}`,
  bodyOrParams: {},
}));

export { shoppingCartService };
