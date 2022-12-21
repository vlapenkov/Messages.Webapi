<template>
  <app-page id="filter-container" class="relative" title="Каталог товаров">
    <template #subheader> </template>
    <div class="flex flex-row justify-content-end">
      <sort-by-container></sort-by-container>
    </div>
    <div class="grid mt-1">
      <div v-if="showFilters" class="col-3 gap-2 flex flex-column"></div>
      <div ref="productsContainerRef" :class="{ 'col-9': showFilters, 'col-12': !showFilters }">
        <products-viewer />
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import {
  IproductionsPageRequest,
  ProductStatus,
} from '@/app/productions/@types/IproductionsPageRequest';
import { productionsService } from '@/app/productions/services/productions.service';
import { productionsStore } from '@/app/productions/state/productions.store';
import { useElementSize } from '@vueuse/core';
import { defineComponent, onMounted, ref, watch } from 'vue';
import { useRouteQueryBinded } from '@/composables/bind-route-query.composable';
import { isNullOrEmpty } from '@/tools/string-tools';
import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { onBeforeRouteLeave } from 'vue-router';

export default defineComponent({
  setup() {
    const { sectionId, region, organization, searchQuery, orderBy } = catalogFiltersStore;
    const { pageNumber, pageSize } = productionsStore;

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

    onMounted(() => {
      const request: IproductionsPageRequest = {
        name: isNullOrEmpty(searchQuery.value) ? null : searchQuery.value,
        catalogSectionId: sectionId.value ?? undefined,
        pageNumber: pageNumber.value,
        pageSize: pageSize.value,
        producerName: organization.value ?? null,
        region: region.value ?? null,
        orderBy: orderBy.value,
        status: ProductStatus.Active,
      };
      productionsService.loadPage(request);
    });

    watch([pageNumber, pageSize, orderBy], ([pnum, psize, order]) => {
      productionsService.loadPage({
        name: isNullOrEmpty(searchQuery.value) ? null : searchQuery.value,
        pageNumber: pnum,
        pageSize: psize,
        producerName: organization.value ?? null,
        region: region.value,
        orderBy: order,
        status: ProductStatus.Active,
      });
    });

    onBeforeRouteLeave(() => {
      pageNumber.value = 1;
    });

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
