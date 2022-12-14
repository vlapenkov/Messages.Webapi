<template>
  <app-page title="Каталог товаров">
    <template #subheader> </template>
    <div class="grid mt-1">
      <div v-if="showFilters" class="col-3 gap-2 flex flex-column">
        <div>
          <tree-select
            class="w-full"
            :options="sectionsTree"
            v-model="sectionModelTree"
            placeholder="Область применения"
          ></tree-select>
        </div>
        <div>
          <dropdown
            v-model="regionModel"
            :options="regionOptions"
            show-clear
            placeholder="Регион"
            :style="{ width: '100%' }"
          />
        </div>
        <div>
          <dropdown
            show-clear
            v-model="organizationModel"
            :options="organizationOptions"
            placeholder="Производитель"
            :style="{ width: '100%' }"
          />
        </div>
      </div>
      <div ref="productsContainerRef" :class="{ 'col-9': showFilters, 'col-12': !showFilters }">
        <products-viewer />
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import { IproductionsPageRequest } from '@/app/productions/@types/IproductionsPageRequest';
import { productionsService } from '@/app/productions/services/productions.service';
import { productionsStore } from '@/app/productions/state/productions.store';
import { useElementSize } from '@vueuse/core';
import { defineComponent, ref, watch } from 'vue';
import { useRouteQueryBinded } from '@/composables/bind-route-query.composable';
import { isNullOrEmpty } from '@/tools/string-tools';
import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { useCatalogFilters } from '@/composables/catalog-filters.composable';
import { viewModeProvider } from './providers/view-mode.provider';

export default defineComponent({
  setup() {
    const viewMode = viewModeProvider.provide();
    const { sectionId, region, organization, searchQuery } = catalogFiltersStore;

    const switchViewMode = () => {
      viewMode.value = viewMode.value === 'user' ? 'admin' : 'user';
    };

    useRouteQueryBinded('sectionId', {
      type: 'number',
      ref: sectionId,
    });

    useRouteQueryBinded('region', {
      type: 'string',
      ref: region,
    });

    useRouteQueryBinded('organization', {
      type: 'string',
      ref: organization,
    });

    useRouteQueryBinded('searchQuery', {
      type: 'string',
      ref: searchQuery,
    });

    watch(
      [
        productionsStore.pageNumber,
        productionsStore.pageSize,
        searchQuery,
        sectionId,
        region,
        organization,
      ],
      ([pageNumber, pageSize, query, catalogSectionId, reg, org]) => {
        // console.log('Запрашиваем страницы', pageNumber, pageSize, query, catalogSectionId);
        const request: IproductionsPageRequest = {
          name: isNullOrEmpty(query) ? null : query,
          catalogSectionId: catalogSectionId ?? undefined,
          pageNumber,
          pageSize,
          producerName: org ?? null,
          region: reg ?? null,
        };

        productionsService.loadPage(request);
      },
      {
        immediate: true,
      },
    );

    const productsContainerRef = ref<HTMLElement>();
    const { width: productsContainerSize } = useElementSize(productsContainerRef);

    const { regionOptions, organizationOptions, showFilters, sectionsTree, sectionModelTree } =
      useCatalogFilters();

    return {
      sectionId,
      sectionsTree,
      sectionModelTree,
      search: searchQuery,
      productsContainerRef,
      productsContainerSize,
      viewMode,
      switchViewMode,
      regionModel: region,
      organizationModel: organization,
      regionOptions,
      organizationOptions,
      showFilters,
    };
  },
});
</script>

<style lang="scss" scoped>
.reshape-tree {
  :deep(.p-tree) {
    margin-top: 0 !important;
    border: none;
  }
}
</style>
