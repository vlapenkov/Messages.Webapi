import { definePageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/pageable-collection.store';
import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { pageNumber } from '@/app/core/services/harlem/state/decorators/page-number.decorator';
import { pageRequest } from '@/app/core/services/harlem/state/decorators/page-request.decorator';
import { pageSize } from '@/app/core/services/harlem/state/decorators/page-size-decorator';
import { pages } from '@/app/core/services/harlem/state/decorators/pages.decorator';
import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { productsHttpService } from '../infrastructure/products.http-service';
import { ProductShortModel } from '../models/product.model';

export interface IproductsPageRequest extends IPagedRequest {
  catalogSectionId?: number;
  name: string | null;
}

export class ProductsState extends StateBase {
  @pages
  pages: IPagedResponse<ProductShortModel>[] = [];

  @pageNumber
  pageNumber = 0;

  @pageSize
  pageSize = 0;

  @pageRequest
  pageRequest(): IproductsPageRequest {
    return {
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
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
