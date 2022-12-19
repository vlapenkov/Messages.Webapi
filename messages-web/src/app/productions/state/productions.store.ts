import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { computed } from 'vue';
import { ProductionModel } from '../models/production.model';
import { ProductShortsState } from './productions.state';

const { getter, mutation, computeState } = defineStore('products', new ProductShortsState());

const pageNumber = computeState((state) => state.pageNumber);
const pageSize = computeState((state) => state.pageSize);
const pages = computeState((state) => state.pages);
const orderBy = computeState((state) => state.orderBy);

const currentPage = getter('get-current-page', (state) => {
  const foundedPage = state.pages.find((s) => s.pageNumber === state.pageNumber);
  return foundedPage ?? null;
});

const currentPageItemsGet = getter<readonly ProductionModel[] | null>(
  'current-page--items--get',
  () => (currentPage.value?.rows as ProductionModel[] | undefined) ?? null,
);
const currentPageItemsSet = mutation<readonly ProductionModel[]>(
  'current-page--items--set',
  (state, payload) => {
    const foundedPage = state.pages.find((s) => s.pageNumber === state.pageNumber);
    if (foundedPage == null) {
      return;
    }
    foundedPage.rows = payload.map((i) => i.clone());
  },
);

const currentPageItems = computed({
  get: () => currentPageItemsGet.value,
  set: (val) => {
    if (val == null) {
      return;
    }
    currentPageItemsSet(val);
  },
});

const status = computeState((state) => state.status);

const selectedItem = computeState((state) => state.selectedItem);

const showFilters = computeState((state) => state.showFilters);

const selectedItemMode = getter('selected-item--mode', (state) => state.selectedItem?.mode ?? null);

const insertPage = mutation<IPagedResponse<ProductionModel>>('insert-page', (state, payload) => {
  const pageIndex = state.pages.findIndex(
    (p) => p.pageNumber === payload.pageNumber && p.pageSize === payload.pageSize,
  );
  if (pageIndex !== -1) {
    state.pages[pageIndex] = payload;
  } else {
    state.pages.push(payload);
  }
});

const setPage = mutation<IPagedResponse<ProductionModel>>('set-page', (state, payload) => {
  state.pages = [payload];
});

export const productionsStore = {
  orderBy,
  currentPage,
  currentPageItems,
  status,
  selectedItem,
  selectedItemMode,
  showFilters,
  pageNumber,
  pageSize,
  pages,
  insertPage,
  setPage,
};
