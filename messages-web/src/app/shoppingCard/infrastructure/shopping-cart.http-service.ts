import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';

const [shoppingCartService] = defineCollectionService({
  url: 'api/v1/ShoppingCart',
});

export { shoppingCartService };
