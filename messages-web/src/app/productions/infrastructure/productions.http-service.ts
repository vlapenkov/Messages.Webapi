import { definePageableCollectionService } from '@/app/core/services/http/custom/pageable-collection.http-service';

const [productShortsHttpService] = definePageableCollectionService({
  url: 'api/Products',
});

export { productShortsHttpService };
