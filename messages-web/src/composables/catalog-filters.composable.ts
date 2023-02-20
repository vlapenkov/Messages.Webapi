import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { isNullOrEmpty } from '@/tools/string-tools';
import { computed, ref, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { useOrganizations } from './organizations.composable';
import { useSections } from './sections.composable';

export function useCatalogFilters() {
  const router = useRouter();
  const route = useRoute();
  const { list: sectionsList, tree: sectionsTree } = useSections();
  const { organizationOptions, regionOptions } = useOrganizations();
  const sectionOptions = computed(() =>
    (sectionsList.value ?? []).map((s) => ({ label: s.name, value: s.id })),
  );

  const organization = ref<string | null>(null);
  watch(
    catalogFiltersStore.organization,
    (o) => {
      if (organization.value !== o) {
        organization.value = o;
      }
    },
    {
      immediate: true,
    },
  );
  const region = ref<string | null>(null);
  watch(
    catalogFiltersStore.region,
    (r) => {
      if (region.value !== r) {
        region.value = r;
      }
    },
    {
      immediate: true,
    },
  );
  const sectionId = ref<number | null>(null);
  watch(
    catalogFiltersStore.sectionId,
    (s) => {
      if (sectionId.value !== s) {
        sectionId.value = s;
      }
    },
    {
      immediate: true,
    },
  );

  const sectionModel = computed<{ label: string; value: number } | undefined>({
    get: () => {
      const selectedSection = (sectionsList.value ?? []).find((s) => s.id === sectionId.value);
      if (selectedSection == null) {
        return undefined;
      }
      return {
        label: selectedSection.name,
        value: selectedSection.id,
      };
    },
    set: (val) => {
      sectionId.value = val?.value ?? null;
    },
  });

  const sectionModelTree = computed<Record<string, boolean> | undefined>({
    get: () => (sectionId.value == null ? undefined : { [`${sectionId.value}`]: true }),
    set: (val) => {
      if (val == null) {
        sectionId.value = null;
        return;
      }
      Object.keys(val).forEach((key) => {
        if (val[key] === true) {
          sectionId.value = +key;
        }
      });
    },
  });

  const searchForProducts = () => {
    catalogFiltersStore.sectionId.value = sectionId.value;
    catalogFiltersStore.region.value = region.value;
    catalogFiltersStore.organization.value = organization.value;
    catalogFiltersStore.searchQuery.value = catalogFiltersStore.searchQueryDraft.value;
    if (['catalog', 'products', 'org-products'].every((i) => i !== route.name)) {
      router.push({
        name: 'catalog',
        query: {
          sectionId: catalogFiltersStore.sectionId.value,
          region: catalogFiltersStore.region.value,
          organization: catalogFiltersStore.organization.value,
          searchQuery: isNullOrEmpty(catalogFiltersStore.searchQuery.value)
            ? null
            : catalogFiltersStore.searchQuery.value,
        },
      });
    }
    catalogFiltersStore.showFilters.value = false;
  };

  return {
    sectionsTree,
    sectionOptions,
    organizationOptions,
    regionOptions,
    sectionModel,
    sectionModelTree,
    searchQuery: catalogFiltersStore.searchQueryDraft,
    showFilters: catalogFiltersStore.showFilters,
    organization,
    region,
    searchForProducts,
  };
}
