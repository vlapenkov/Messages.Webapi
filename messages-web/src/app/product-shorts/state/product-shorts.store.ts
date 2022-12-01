import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { computed } from 'vue';
import { ProductShortModel } from '../models/product-short.model';
import { ProductShortsState } from './product-shorts.state';

const { getter, mutation, computeState } = defineStore('products', new ProductShortsState());

const pageNumber = computeState((state) => state.pageNumber);
const pageSize = computeState((state) => state.pageSize);
const pages = computeState((state) => state.pages);

const currentPage = getter('get-current-page', (state) => {
  const foundedPage = state.pages.find((s) => s.pageNumber === state.pageNumber);
  return foundedPage ?? null;
});

const currentPageItemsGet = getter<readonly ProductShortModel[] | null>(
  'current-page--items--get',
  () => (currentPage.value?.rows as ProductShortModel[] | undefined) ?? null,
);
const currentPageItemsSet = mutation<readonly ProductShortModel[]>(
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

const selectedItemMode = getter('selected-item--mode', (state) => state.selectedItem?.mode ?? null);

const parentSectionId = computeState((state) => state.sectionId);

const searchQuery = computeState((state) => state.searchQuery);

const insertPage = mutation<IPagedResponse<ProductShortModel>>('insert-page', (state, payload) => {
  const pageIndex = state.pages.findIndex(
    (p) => p.pageNumber === payload.pageNumber && p.pageSize === payload.pageSize,
  );
  if (pageIndex !== -1) {
    state.pages[pageIndex] = payload;
  } else {
    state.pages.push(payload);
  }
});

const setPage = mutation<IPagedResponse<ProductShortModel>>('set-page', (state, payload) => {
  state.pages = [payload];
});

export const productShortsStore = {
  currentPage,
  currentPageItems,
  status,
  selectedItem,
  selectedItemMode,
  pageNumber,
  pageSize,
  pages,
  parentSectionId,
  searchQuery,
  insertPage,
  setPage,
};
