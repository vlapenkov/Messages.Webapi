<template>
  <app-page id="filter-container" class="relative" title="Каталог товаров">
    <template #subheader> </template>
    <div class="grid mt-1">
      <div v-if="showFilters" class="col-3 gap-2 flex flex-column"></div>
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

export default defineComponent({
  setup() {
    const { sectionId, region, organization, searchQuery } = catalogFiltersStore;

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

    return {
      sectionId,
      showFilters: productionsStore.showFilters,
      search: searchQuery,
      productsContainerRef,
      productsContainerSize,
      regionModel: region,
      organizationModel: organization,
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
