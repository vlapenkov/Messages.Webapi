import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { ProductShortModel } from '../models/product-short.model';

export class ProductShortsState extends StateBase {
  pages: IPagedResponse<ProductShortModel>[] = [];

  status = new DataStatus();

  searchQuery: string | null = null;

  sectionId: number | undefined;

  region: string | undefined;

  organization: string | undefined;

  pageNumber = 1;

  pageSize = 15;

  selectedItem: NotValidData<ProductShortModel> | null = null;
}
