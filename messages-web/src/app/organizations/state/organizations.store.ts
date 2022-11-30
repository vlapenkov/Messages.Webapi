import { defineStore } from '@/app/core/services/harlem/harlem.service';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { computed } from 'vue';
import { OrganizationModel } from '../model/organization.model';
import { OrganizationsState } from './organizations.state';

const { getter, mutation, computeState } = defineStore('organizations', new OrganizationsState());

const pageNumber = computeState((state) => state.pageNumber);
const pageSize = computeState((state) => state.pageSize);
const pages = computeState((state) => state.pages);

const currentPage = getter('get-current-page', (state) => {
  const foundedPage = state.pages.find((s) => s.pageNumber === state.pageNumber);
  return foundedPage ?? null;
});

const currentPageItemsGet = getter<readonly OrganizationModel[] | null>(
  'current-page--items--get',
  () => (currentPage.value?.rows as OrganizationModel[] | undefined) ?? null,
);
const currentPageItemsSet = mutation<readonly OrganizationModel[]>(
  'current-page--items--set',
  (state, payload) => {
    const foundedPage = state.pages.find((s) => s.pageNumber === state.pageNumber);
    if (foundedPage == null) {
      return;
    }
    foundedPage.rows = payload.map((i) => i.clone() as OrganizationModel);
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

const insertPage = mutation<IPagedResponse<OrganizationModel>>('insert-page', (state, payload) => {
  const pageIndex = state.pages.findIndex(
    (p) => p.pageNumber === payload.pageNumber && p.pageSize === payload.pageSize,
  );
  if (pageIndex !== -1) {
    state.pages[pageIndex] = payload;
  } else {
    state.pages.push(payload);
  }
});

export const organizationsStore = {
  currentPage,
  currentPageItems,
  status,
  pageNumber,
  pageSize,
  pages,
  insertPage,
};
