import { defineSingleHttpService } from '@/app/core/services/http/custom/single.http-service';
import { IProductFullModel } from '../@types/IProductFullModel';

const [productFullService, { defineGet }] = defineSingleHttpService<IProductFullModel>({
  url: 'api/v1/Products',
});

productFullService.get = defineGet((arg: number) => ({
  url: `/${arg}`,
}));

export { productFullService };
