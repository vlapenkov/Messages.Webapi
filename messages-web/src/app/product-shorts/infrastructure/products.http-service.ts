import { definePageableCollectionService } from '@/app/core/services/http/custom/pageable-collection.http-service';

const [productsHttpService] = definePageableCollectionService({
  url: 'api/v1/Products',
});

export { productsHttpService };
