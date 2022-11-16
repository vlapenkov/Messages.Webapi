import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { dataStatus } from '@/app/core/services/harlem/state/decorators/data-status.decorator';
import { pageNumber } from '@/app/core/services/harlem/state/decorators/page-number.decorator';
import { pageRequest } from '@/app/core/services/harlem/state/decorators/page-request.decorator';
import { pageSize } from '@/app/core/services/harlem/state/decorators/page-size-decorator';
import { pages } from '@/app/core/services/harlem/state/decorators/pages.decorator';
import { selected } from '@/app/core/services/harlem/state/decorators/selected-item.decorator';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { ProductShortModel } from '../models/product-short.model';
import { IproductsPageRequest } from '../@types/IproductsPageRequest';

export class ProductShortsState extends StateBase {
  @pages
  pages: IPagedResponse<ProductShortModel>[] = [];

  @dataStatus
  status = new DataStatus();

  @pageNumber
  pageNumber = 1;

  @pageSize
  pageSize = 15;

  sectionId: number | undefined;

  @pageRequest
  // eslint-disable-next-line class-methods-use-this
  pageRequest(state: ProductShortsState): IproductsPageRequest {
    return {
      pageNumber: state.pageNumber,
      pageSize: state.pageSize,
      catalogSectionId: state.sectionId,
      name: null,
    };
  }

  @selected({
    create: true,
    delete: false,
    update: false,
  })
  selectedItem: NotValidData<ProductShortModel> | null = null;
}
