import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { IAttribute } from '../@types/IAttribute';

const [attributeHttpService, { defineGet }] = defineCollectionService<IAttribute>({
  url: 'api/Products',
});

attributeHttpService.get = defineGet(() => ({
  url: `/attributes`,
}));

export { attributeHttpService };
