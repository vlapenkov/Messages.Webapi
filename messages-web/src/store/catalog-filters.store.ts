import { defineStore } from '@/app/core/services/harlem/harlem.service';

export interface ICatalogFilterState {
  serachQuery: string | null;
  region: string | null;
  organization: string | null;
  sectionId: number | null;
}

const catalogFiltersDefault: ICatalogFilterState = {
  serachQuery: null,
  region: null,
  organization: null,
  sectionId: null,
};

const { computeState } = defineStore('search-filters', catalogFiltersDefault);

const searchQuery = computeState((state) => state.serachQuery);

const region = computeState((state) => state.region);

const organization = computeState((state) => state.organization);

const sectionId = computeState((state) => state.sectionId);

export const catalogFiltersStore = {
  searchQuery,
  region,
  organization,
  sectionId,
};
