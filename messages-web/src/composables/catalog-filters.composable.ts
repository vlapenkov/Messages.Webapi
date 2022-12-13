import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { computed } from 'vue';
import { useRouter } from 'vue-router';
import { useOrganizations } from './organizations.composable';
import { useSections } from './sections.composable';

export function useCatalogFilters() {
  const router = useRouter();
  const { list: sections } = useSections();
  const { organizations: organizationOptions, regions: regionOptions } = useOrganizations();
  const sectionOptions = computed(() =>
    (sections.value ?? []).map((s) => ({ label: s.name, value: s.id })),
  );

  const sectionModel = computed({
    get: () => {
      const selectedSection = (sections.value ?? []).find(
        (s) => s.id === catalogFiltersStore.sectionId.value,
      );
      if (selectedSection == null) {
        return undefined;
      }
      return {
        label: selectedSection.name,
        value: selectedSection.id,
      };
    },
    set: (val) => {
      catalogFiltersStore.sectionId.value = val?.value ?? null;
    },
  });

  const {
    region: regionModel,
    organization: organizationModel,
    searchQuery,
    showFilters,
  } = catalogFiltersStore;

  const searchForProducts = () => {
    router.push({
      name: 'catalog',
      query: {
        sectionId: catalogFiltersStore.sectionId.value,
        region: regionModel.value,
        organization: organizationModel.value,
        searchQuery: searchQuery.value,
      },
    });
  };

  return {
    sectionOptions,
    organizationOptions,
    regionOptions,
    sectionModel,
    searchQuery,
    showFilters,
    organizationModel,
    regionModel,
    searchForProducts,
  };
}
