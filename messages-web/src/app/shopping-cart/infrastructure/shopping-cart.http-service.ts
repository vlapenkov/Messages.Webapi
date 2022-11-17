import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { IAddToShoppingCartRequest } from '../@types/IAddToShoppingCartRequest';

const [shoppingCartService, { definePost }] = defineCollectionService({
  url: 'api/v1/ShoppingCart',
});

export const addToCard = definePost<void, IAddToShoppingCartRequest>();

export { shoppingCartService };
