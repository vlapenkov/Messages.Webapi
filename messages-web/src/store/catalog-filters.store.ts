import { defineStore } from '@/app/core/services/harlem/harlem.service';

export enum ProductionsOrder {
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
  serachQueryDraft: string | null;
  region: string | null;
  organization: string | null;
  sectionId: number | null;
  showFilters: boolean;
}

const catalogFiltersDefault: ICatalogFilterState = {
  serachQueryDraft: null,
  serachQuery: null,
  region: null,
  organization: null,
  sectionId: null,
  showFilters: false,
};

const { computeState, mutation } = defineStore('search-filters', catalogFiltersDefault);

const searchQuery = computeState((state) => state.serachQuery);

const searchQueryDraft = computeState((state) => state.serachQueryDraft);

const region = computeState((state) => state.region);

const organization = computeState((state) => state.organization);

const sectionId = computeState((state) => state.sectionId);

const showFilters = computeState((state) => state.showFilters);

const undoMainFilters = mutation('undo-main-filters', (state) => {
  state.region = null;
  state.sectionId = null;
  state.organization = null;
  state.serachQueryDraft = null;
  state.serachQuery = null;
});

export const catalogFiltersStore = {
  searchQuery,
  searchQueryDraft,
  region,
  organization,
  sectionId,
  showFilters,
  undoMainFilters,
};
