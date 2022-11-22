import { definePageableCollectionService } from '@/app/core/services/http/custom/pageable-collection.http-service';

const [productsHttpService] = definePageableCollectionService({
  url: 'api/Products',
});

export { productsHttpService };
