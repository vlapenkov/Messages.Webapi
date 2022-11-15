import { defineSingleHttpService } from '@/app/core/services/http/custom/single.http-service';

export const [productFullService] = defineSingleHttpService({
  url: 'api/v1/Products',
});
