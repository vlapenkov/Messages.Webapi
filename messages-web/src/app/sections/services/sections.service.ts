// import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
// import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
// import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
// import { sectionsHttpService } from '../infrastructure/sections.http-service';
// import { IOrderModel } from '../model/IOrderModel';
// import { OrderModel } from '../model/order.model';
// import { ordersStore } from '../state/orders.store';
// import { sectionsStore } from '../state/sections.store';

// async function loadSections() {
//   sectionsStore.  .status.value = new DataStatus('loading');
//   const response = await sectionsHttpService.get();
//   if (response.status === HttpStatus.Success && response.data != null) {
//     const model = response.data.rows.map((i) => {
//       const parsedData = new OrderModel();
//       const parseSuccess = parsedData.fromResponse(i as IOrderModel);
//       if (parseSuccess) {
//         return parsedData;
//       }
//       throw new Error('Не удалось преобразовать данные');
//     });
//     const newItem: IPagedResponse<OrderModel> = { ...response.data, rows: model };
//     sectionsStore.status.value = new DataStatus('loaded');
//     sectionsStore.insertPage(newItem);
//   }
// }

// export const sectionsService = {
//   loadSections,
// };
