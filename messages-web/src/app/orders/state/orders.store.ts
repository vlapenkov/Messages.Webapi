import { definePageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/pageable-collection.store';
import { ordersHttpService } from '../infrastructure/order.http-service';
import { OrderModel } from '../model/order.model';
import { OrdersState } from './orders.state';

export const [ordersStore] = definePageableCollectionStore(
  'orders',
  OrderModel,
  OrdersState,
  ordersHttpService,
);
