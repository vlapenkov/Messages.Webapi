import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { OrderByProduct } from '../models/OrderByProduct';
import { ProductionModel } from '../models/production.model';

export class ProductShortsState extends StateBase {
  pages: IPagedResponse<ProductionModel>[] = [];

  status = new DataStatus();

  pageNumber = 1;

  pageSize = 16;

  showFilters = false;

  selectedItem: NotValidData<ProductionModel> | null = null;

  orderBy: OrderByProduct | null = null;
}
