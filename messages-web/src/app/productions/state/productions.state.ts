import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { Page } from '@/app/core/services/harlem/state/tools/page';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { IproductionsPageRequest } from '../@types/IproductionsPageRequest';
import { ProductionModel } from '../models/production.model';

export class ProductShortsState extends StateBase {
  pages: Page<IproductionsPageRequest, IPagedResponse<ProductionModel>>[] = [];

  selectedItem: NotValidData<ProductionModel> | null = null;
}
