import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { Page } from '@/app/core/services/harlem/state/tools/page';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { computed, DeepReadonly } from 'vue';
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

const changePage = mutation<Page<IproductionsPageRequest, IPagedResponse<ProductionModel>>>(
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

const getPageState = (request: IproductionsPageRequest) => {
  const page = computed({
    get: () =>
      productionsState.pages.find((p) =>
        Object.keys(p.request).every(
          (key) =>
            p.request[key as keyof IproductionsPageRequest] ===
            request[key as keyof IproductionsPageRequest],
        ),
      ),
    set: (val) => {
      if (val == null || val.data == null) {
        return;
      }
      changePage({
        data: {
          ...val.data,
          rows: val.data.rows.map((r) => {
            const m = new ProductionModel();
            Object.assign(m, r);
            return m;
          }),
        },
        request: { ...val.request },
        status: new DataStatus(val.status.status, val.status.message),
      });
    },
  });
  const pageStatus = computed({
    get: () => page.value?.status,
    set: (sv) => {
      if (page.value == null || sv == null) {
        return;
      }
      page.value = { ...page.value, status: sv };
    },
  });
  const pageData = computed<DeepReadonly<IPagedResponse<ProductionModel>> | undefined>({
    get: () => page.value?.data,
    set: (dv) => {
      if (page.value == null) {
        return;
      }
      page.value = { ...page.value, data: dv };
    },
  });
  return {
    page,
    pageData,
    pageStatus,
  };
};

const selectedItemMode = getter('Selected Item Mode', (state) => state.selectedItem?.mode ?? null);

const loadPage = action<IproductionsPageRequest>('Load New Productions Page', async (request) => {
  addEmptyPage(request);
  const { pageData, pageStatus } = getPageState(request);
  pageStatus.value = new DataStatus('loading');

  const response = await productionsHttpService.getPage(request);
  if (response.status === HttpStatus.Success && response.data != null) {
    const model = response.data.rows.map((i) => {
      const parsedData = new ProductionModel();
      const parseSuccess = parsedData.fromResponse(i as IProductionModel);
      if (parseSuccess) {
        return parsedData;
      }
      pageStatus.value = new DataStatus('error');
      throw new Error('Не удалось преобразовать данные');
    });
    const newItem: IPagedResponse<ProductionModel> = { ...response.data, rows: model };
    pageData.value = newItem as DeepReadonly<IPagedResponse<ProductionModel>>;
    pageStatus.value = new DataStatus('loaded');
  }
});

export const productionsStore = {
  getPageState,
  selectedItem,
  selectedItemMode,
  pages,
  loadPage,
};
