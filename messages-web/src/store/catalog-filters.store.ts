import { defineStore } from '@/app/core/services/harlem/harlem.service';

export enum OrderByProduct {
  NameByAsc,
  NameByDesc,
  RegionByAsc,
  RegionByDesc,
  ProducerByAsc,
  ProducerByDesc,
  RatingByAsc,
  RatingByDesc,
  IdByAsc,
  IdByDesc,
}

export interface ICatalogFilterState {
  serachQuery: string | null;
  region: string | null;
  organization: string | null;
  sectionId: number | null;
  showFilters: boolean;
  orderBy: OrderByProduct | null;
}

const catalogFiltersDefault: ICatalogFilterState = {
  serachQuery: null,
  region: null,
  organization: null,
  sectionId: null,
  showFilters: false,
  orderBy: OrderByProduct.NameByAsc,
};

const { computeState, mutation } = defineStore('search-filters', catalogFiltersDefault);

const searchQuery = computeState((state) => state.serachQuery);

const region = computeState((state) => state.region);

const organization = computeState((state) => state.organization);

const sectionId = computeState((state) => state.sectionId);

const showFilters = computeState((state) => state.showFilters);

const orderBy = computeState((state) => state.orderBy);

const undoMainFilters = mutation('undo-main-filters', (state) => {
  state.region = null;
  state.sectionId = null;
  state.organization = null;
  state.serachQuery = null;
});

export const catalogFiltersStore = {
  searchQuery,
  region,
  organization,
  sectionId,
  showFilters,
  undoMainFilters,
  orderBy,
};
