import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { IAddToShoppingCartRequest } from '../@types/IAddToShoppingCartRequest';
import { IShoppingCartModel } from '../@types/IShoppingCartModel';

const [service, { definePost, defineDelete }] = defineCollectionService<IShoppingCartModel>({
  url: 'api/ShoppingCart',
});

const addToCart = definePost<void, IAddToShoppingCartRequest>();

service.del = defineDelete((model) => ({
  url: `/${model.productId}`,
  bodyOrParams: {},
}));

const createOrder = definePost<void, void>();

export const shoppingCartHttpService = { ...service, addToCart, createOrder };
