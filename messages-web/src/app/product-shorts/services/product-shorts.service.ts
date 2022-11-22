import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { watch } from 'vue';
import { IproductsPageRequest } from '../@types/IproductsPageRequest';
import { productShortsHttpService } from '../infrastructure/product-shorts.http-service';
import { IProductShortModel, ProductShortModel } from '../models/product-short.model';
import { productShortsStore } from '../state/product-shorts.store';

async function loadPage(request: IproductsPageRequest) {
  const response = await productShortsHttpService.getPage(request);
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.rows.map((i) => {
      const parsedData = new ProductShortModel();
      const parseSuccess = parsedData.fromResponse(i as IProductShortModel);
      if (parseSuccess) {
        return parsedData;
      }
      throw new Error('Не удалось преобразовать данные');
    });
    const newItem: IPagedResponse<ProductShortModel> = { ...response.data, rows: model };
    productShortsStore.insertPage(newItem);
  }
}

watch(
  [
    productShortsStore.pageNumber,
    productShortsStore.pageSize,
    productShortsStore.searchQuery,
    productShortsStore.parentSectionId,
  ],
  ([pageNumber, pageSize, query, catalogSectionId]) => {
    const request: IproductsPageRequest = {
      name: query == null || query === '' || query.trim() === '' ? null : query,
      catalogSectionId,
      pageNumber,
      pageSize,
    };
    loadPage(request);
  },
);

export const productShortsService = {
  loadPage,
};
