import { definePageableCollectionService } from '@/app/core/services/http/custom/pageable-collection.http-service';
import { IOrderModel } from '../model/IOrderModel';

const [ordersHttpService] = definePageableCollectionService<IOrderModel>({
  url: 'api/v1/Orders',
});

export { ordersHttpService };
