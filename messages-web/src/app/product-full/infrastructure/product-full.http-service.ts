import { defineSingleHttpService } from '@/app/core/services/http/custom/single.http-service';
import { IProductFullModel } from '../@types/IProductFullModel';

export const [productFullService] = defineSingleHttpService<IProductFullModel>({
  url: 'api/v1/Products',
});
