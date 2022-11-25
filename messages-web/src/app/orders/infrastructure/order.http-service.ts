import { definePageableCollectionService } from '@/app/core/services/http/custom/pageable-collection.http-service';
import { IOrderModel, IOrderModelFull } from '../model/IOrderModel';

const [ordersHttpService, { defineGet }] = definePageableCollectionService<IOrderModel>({
  url: 'api/Orders',
});

export { ordersHttpService };

export const getOrder = defineGet<IOrderModelFull, number>((id) => ({
  url: `/${id}`,
}));
