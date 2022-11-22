import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { ProductShortModel } from '../models/product-short.model';

export class ProductShortsState extends StateBase {
  pages: IPagedResponse<ProductShortModel>[] = [];

  status = new DataStatus();

  searchQuery: string | null = null;

  pageNumber = 1;

  pageSize = 15;

  sectionId: number | undefined;

  // pageRequest(state: ProductShortsState): IproductsPageRequest {
  //   return {
  //     pageNumber: state.pageNumber,
  //     pageSize: state.pageSize,
  //     catalogSectionId: state.sectionId,
  //     name: null,
  //   };
  // }

  selectedItem: NotValidData<ProductShortModel> | null = null;
}
