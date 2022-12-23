import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { Page } from '@/app/core/services/harlem/state/tools/page';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { computed } from 'vue';
import { IproductionsPageRequest } from '../@types/IproductionsPageRequest';
import { productionsHttpService } from '../infrastructure/productions.http-service';
import { IProductionModel, ProductionModel } from '../models/production.model';
import { ProductShortsState } from './productions.state';

const {
  state: productionsState,
  getter,
  computeState,
  mutation,
  action,
} = defineStore('products', new ProductShortsState());

const pages = computeState((state) => state.pages);

const addEmptyPage = mutation<IproductionsPageRequest>('Creating Empty Page', (state, payload) => {
  state.pages.push(new Page(payload));
});

const selectedItem = computeState((state) => state.selectedItem);

const changePage = mutation<Page<IproductionsPageRequest, IPagedResponse<IProductionModel>>>(
  'Change Page',
  (state, payload) => {
    const index = state.pages.findIndex((page) =>
      Object.keys(page.request).every(
        (key) =>
          page.request[key as keyof IproductionsPageRequest] ===
          payload.request[key as keyof IproductionsPageRequest],
      ),
    );
    if (index === -1) {
      state.pages.push(payload);
    } else {
      state.pages[index] = payload;
    }
  },
);

const getPageData = (request: IproductionsPageRequest) => {
  const getPage = computed({
    get: () =>
      productionsState.pages.find((page) =>
        Object.keys(page.request).every(
          (key) =>
            page.request[key as keyof IproductionsPageRequest] ===
            request[key as keyof IproductionsPageRequest],
        ),
      ),
    set: (val) => {
      if (val == null) {
        return;
      }
      changePage({ ...val });
    },
  });
  throw new Error('Not Implemented!');
};

const selectedItemMode = getter('Selected Item Mode', (state) => state.selectedItem?.mode ?? null);

const loadPage = action<IproductionsPageRequest>('Load New Productions Page', async (request) => {
  addEmptyPage(request);
  const pageToFill = getPage(request);
  if (pageToFill.value == null) {
    return;
  }
  const response = await productionsHttpService.getPage(request);
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.rows.map((i) => {
      const parsedData = new ProductionModel();
      const parseSuccess = parsedData.fromResponse(i as IProductionModel);
      if (parseSuccess) {
        return parsedData;
      }
      productionsStore.status.value = new DataStatus('error');
      throw new Error('Не удалось преобразовать данные');
    });
    const newItem: IPagedResponse<ProductionModel> = { ...response.data, rows: model };
    productionsStore.setPage(newItem);
    productionsStore.status.value = new DataStatus('loaded');
  }
});

export const productionsStore = {
  selectedItem,
  selectedItemMode,
  pages,
  loadPage,
};
