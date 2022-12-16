import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { IOrderPagedRequest } from '../@types/IPagedRequest';
import { ordersHttpService } from '../infrastructure/order.http-service';
import { IOrderModel } from '../model/IOrderModel';
import { OrderModel } from '../model/order.model';
import { ordersStore } from '../state/orders.store';

async function loadPage(request: IOrderPagedRequest) {
  ordersStore.status.value = new DataStatus('loading');
  const response = await ordersHttpService.getPage(request);
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.rows.map((i) => {
      const parsedData = new OrderModel();
      const parseSuccess = parsedData.fromResponse(i as IOrderModel);
      if (parseSuccess) {
        return parsedData;
      }
      throw new Error('Не удалось преобразовать данные');
    });
    const newItem: IPagedResponse<OrderModel> = { ...response.data, rows: model };
    ordersStore.status.value = new DataStatus('loaded');
    ordersStore.insertPage(newItem);
  }
}

async function updateStatus(id: number, status: number) {
  try {
    await ordersHttpService.updateStaus(id, status);
  } catch (error) {
    return false;
  }
  return true;
}

export const ordersService = {
  loadPage,
  updateStatus,
};

