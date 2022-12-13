import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { TreeNode } from 'primevue/tree';
import { computed } from 'vue';
import { useRouter } from 'vue-router';
import { useOrganizations } from './organizations.composable';
import { useSections } from './sections.composable';

// eslint-disable-next-line @typescript-eslint/no-unused-vars
function findInTree(
  tree: TreeNode[] | null,
  predicate: (node: TreeNode) => boolean,
): TreeNode | null {
  if (tree == null) {
    return null;
  }
  const findInSingle = (currentNode: TreeNode): TreeNode | null => {
    const checkResult = predicate(currentNode);
    if (checkResult) {
      return currentNode;
    }
    if (currentNode.children != null) {
      const nodeInChildren: TreeNode | undefined = currentNode.children
        .map((child) => findInSingle(child))
        .find((i): i is TreeNode => i != null);
      return nodeInChildren ?? null;
    }
    return null;
  };
  const resultNode: TreeNode | undefined = tree
    .map((child) => findInSingle(child))
    .find((i): i is TreeNode => i != null);
  return resultNode ?? null;
}

export function useCatalogFilters() {
  const router = useRouter();
  const { list: sectionsList, tree: sectionsTree } = useSections();
  const { organizations: organizationOptions, regions: regionOptions } = useOrganizations();
  const sectionOptions = computed(() =>
    (sectionsList.value ?? []).map((s) => ({ label: s.name, value: s.id })),
  );

  const sectionModel = computed<{ label: string; value: number } | undefined>({
    get: () => {
      const selectedSection = (sectionsList.value ?? []).find(
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

  const sectionModelTree = computed<Record<string, boolean> | undefined>({
    get: () =>
      catalogFiltersStore.sectionId.value == null
        ? undefined
        : { [`${catalogFiltersStore.sectionId.value}`]: true },
    set: (val) => {
      if (val == null) {
        catalogFiltersStore.sectionId.value = null;
        return;
      }
      Object.keys(val).forEach((key) => {
        if (val[key] === true) {
          catalogFiltersStore.sectionId.value = +key;
        }
      });
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
    sectionsTree,
    sectionOptions,
    organizationOptions,
    regionOptions,
    sectionModel,
    sectionModelTree,
    searchQuery,
    showFilters,
    organizationModel,
    regionModel,
    searchForProducts,
  };
}
