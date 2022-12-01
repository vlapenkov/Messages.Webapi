import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { IproductsPageRequest } from '../@types/IproductsPageRequest';
import { productShortsHttpService } from '../infrastructure/product-shorts.http-service';
import { IProductShortModel, ProductShortModel } from '../models/product-short.model';
import { productShortsStore } from '../state/product-shorts.store';

async function loadPage(request: IproductsPageRequest) {
  const response = await productShortsHttpService.getPage(request);
  productShortsStore.status.value = new DataStatus('loading');
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.rows.map((i) => {
      const parsedData = new ProductShortModel();
      const parseSuccess = parsedData.fromResponse(i as IProductShortModel);
      if (parseSuccess) {
        return parsedData;
      }
      productShortsStore.status.value = new DataStatus('error');
      throw new Error('Не удалось преобразовать данные');
    });
    const newItem: IPagedResponse<ProductShortModel> = { ...response.data, rows: model };
    productShortsStore.setPage(newItem);
    productShortsStore.status.value = new DataStatus('loaded');
  }
}

export const productShortsService = {
  loadPage,
};
