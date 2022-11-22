import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { dataStatus } from '@/app/core/services/harlem/state/decorators/data-status.decorator';
import { pageNumber } from '@/app/core/services/harlem/state/decorators/page-number.decorator';
import { pageSize } from '@/app/core/services/harlem/state/decorators/page-size-decorator';
import { pages } from '@/app/core/services/harlem/state/decorators/pages.decorator';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { OrderModel } from '../model/order.model';

export class OrdersState extends StateBase {
  @pages
  pages: IPagedResponse<OrderModel>[] = [];

  @dataStatus
  status = new DataStatus();

  @pageNumber
  pageNumber = 1;

  @pageSize
  pageSize = 12;
}
