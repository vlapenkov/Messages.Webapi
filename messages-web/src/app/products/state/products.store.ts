import { IPageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/IPageableCollectionStore';
import { definePageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/pageable-collection.store';
import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { dataStatus } from '@/app/core/services/harlem/state/decorators/data-status.decorator';
import { pageNumber } from '@/app/core/services/harlem/state/decorators/page-number.decorator';
import { pageRequest } from '@/app/core/services/harlem/state/decorators/page-request.decorator';
import { pageSize } from '@/app/core/services/harlem/state/decorators/page-size-decorator';
import { pages } from '@/app/core/services/harlem/state/decorators/pages.decorator';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { productsHttpService } from '../infrastructure/products.http-service';
import { IProductShortModel, ProductShortModel } from '../models/product.model';

export interface IproductsPageRequest extends IPagedRequest {
  catalogSectionId?: number;
  name: string | null;
}

export class ProductsState extends StateBase {
  @pages
  pages: IPagedResponse<ProductShortModel>[] = [];

  @dataStatus
  status = new DataStatus();

  @pageNumber
  pageNumber = 1;

  @pageSize
  pageSize = 12;

  @pageRequest
  // eslint-disable-next-line class-methods-use-this
  pageRequest(
    state: IPageableCollectionStore<IProductShortModel, ProductShortModel>,
  ): IproductsPageRequest {
    return {
      pageNumber: state.pageNumber.value,
      pageSize: state.pageSize.value,
      catalogSectionId: sectionsStore.itemSelected.value?.data.id,
      name: null,
    };
  }
}

export const productsStore = definePageableCollectionStore(
  'products',
  ProductShortModel,
  ProductsState,
  productsHttpService,
);
