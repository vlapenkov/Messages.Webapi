<template>
  <app-page id="filter-container" class="relative" title="Каталог товаров">
    <template #subheader> </template>
    <div class="flex flex-row justify-content-end">
      <order-by-container v-model="orderBy"></order-by-container>
    </div>
    <div class="grid mt-1">
      <div v-if="showFilters" class="col-3 gap-2 flex flex-column"></div>
      <div ref="productsContainerRef" :class="{ 'col-9': showFilters, 'col-12': !showFilters }">
        <div class="flex flex-column justify-content-between h-full">
          <toast position="top-right" group="tr" />
          <transition-fade>
            <template v-if="productionItems != null">
              <div v-if="productionItems.length > 0" class="grid">
                <div v-if="false" class="col-12">
                  <production-toolbar-container></production-toolbar-container>
                </div>
                <div
                  v-for="item in productionItems"
                  :key="item.id"
                  :class="{
                    'col-3': viewMode === 'grid' && !showFilters,
                    'col-4': viewMode === 'grid' && showFilters,
                    'col-12': viewMode === 'list',
                  }"
                >
                  <production-list-item :production="item" @notify="notifyHandler">
                  </production-list-item>
                </div>
              </div>
              <div
                v-else
                style="background-color: var(--surface-card)"
                class="w-full h-full flex justify-content-center border-round align-items-center"
              >
                <div class="text-center">
                  <i class="pi pi-inbox text-8xl opacity-50"></i>
                  <div class="p-component text-lg mt-3">Товаров не найдено</div>
                </div>
              </div>
            </template>
            <template v-else>
              <div class="grid">
                <div v-for="i in 12" :key="i" class="col-3">
                  <skeleton height="150px" />
                </div>
              </div>
            </template>
          </transition-fade>
          <prime-paginator
            class="mt-2"
            v-if="pageNumber && pageSize && (currentPage?.totalItemCount ?? 0) > 0"
            @page="changePage"
            :rows="pageSize"
            :first="pageSize * (pageNumber - 1)"
            :totalRecords="currentPage?.totalItemCount ?? 0"
          ></prime-paginator>
        </div>
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import {
  IproductionsPageRequest,
  ProductStatus,
} from '@/app/productions/@types/IproductionsPageRequest';
import { productionsStore } from '@/app/productions/state/productions.store';
import { useElementSize } from '@vueuse/core';
import { computed, defineComponent, ref, watch } from 'vue';
import { useRouteQueryBinded } from '@/composables/bind-route-query.composable';
import { isNullOrEmpty } from '@/tools/string-tools';
import { PrimePaginator } from '@/tools/prime-vue-components';
import { useToast } from 'primevue/usetoast';
import Toast from 'primevue/toast';
import { catalogFiltersStore, ProductionsOrder } from '@/store/catalog-filters.store';
import { useToastNotificationHandler } from '@/composables/toast-notification-handler.composable';

export default defineComponent({
  components: { PrimePaginator, Toast },
  setup() {
    const toast = useToast();
    const { sectionId, region, organization, searchQuery, showFilters } = catalogFiltersStore;
    const { loadPage, getPageState } = productionsStore;
    const notifyHandler = useToastNotificationHandler(toast);

    const orderBy = ref(ProductionsOrder.NameByAsc);

    const pageNumber = ref(1);
    const pageSize = ref(16);

    useRouteQueryBinded('pageNumber', {
      type: 'number',
      ref: pageNumber,
    });

    useRouteQueryBinded('pageSize', {
      type: 'number',
      ref: pageSize,
    });

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

    const pageRequest = computed<IproductionsPageRequest>(() => ({
      name: isNullOrEmpty(searchQuery.value) ? null : searchQuery.value,
      pageNumber: pageNumber.value,
      pageSize: pageSize.value,
      producerName: organization.value ?? null,
      ProducerId: null,
      region: region.value,
      orderBy: orderBy.value,
      status: ProductStatus.Active,
      catalogSectionId: sectionId.value,
    }));

    watch(
      pageRequest,
      (r) => {
        loadPage(r);
      },
      {
        immediate: true,
        deep: true,
      },
    );

    const pageState = computed(() => getPageState(pageRequest.value));

    const currentPage = computed(() => pageState.value.pageData.value);

    const productionItems = computed(() => pageState.value.pageData.value?.rows ?? null);

    const productsContainerRef = ref<HTMLElement>();
    const { width: productsContainerSize } = useElementSize(productsContainerRef);

    const viewMode = ref<'grid' | 'list'>('grid');

    const changePage = ({ page }: { page: number }) => {
      pageNumber.value = page + 1;
    };

    return {
      showFilters,
      pageNumber,
      pageSize,
      productsContainerRef,
      productsContainerSize,
      viewMode,
      productionItems,
      notifyHandler,
      changePage,
      currentPage,
      orderBy,
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
