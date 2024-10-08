<template>
  <app-page title="Товары (Менеджер производителя)">
    <template #subheader>
      <div class="flex flex-row justify-content-end">
        <div>
          <prime-button
            class="p-button-sm py-2 px-3"
            icon="pi pi-plus"
            label="Добавить"
            @click="toggleMenu"
            aria-controls="overlay_menu-products_edit"
          ></prime-button>
          <prime-menu id="overlay_menu-products_edit" ref="menu" :model="menuItems" :popup="true" />
        </div>
      </div>
    </template>
    <tab-view ref="tabs">
      <tab-panel header="Все">
        <prime-card shadow="1" shadow-hover="none">
          <div class="grid">
            <div class="col-12 flex flex-row justify-content-end">
              <order-by-container v-model="orderBy"></order-by-container>
            </div>
            <div class="col-12">
              <div v-if="pageStatus?.status === 'loaded'">
                <data-table :value="productions">
                  <column header="Артикул" field="article">
                    <template #body="{ data }">{{ data.article || '123456' }}</template>
                  </column>
                  <column header="Наименование" field="name">
                    <template #body="{ data }">
                      <div class="flex flex-row gap-2 align-items-center">
                        <div style="flex-basis: 100px">
                          <file-store-image
                            fit-width
                            :id="data.documentId"
                            :key="data.documentId"
                          ></file-store-image>
                        </div>
                        <div>{{ data.name }}</div>
                      </div>
                    </template>
                  </column>
                  <column header="Последнее редактирование" field="lastModifiedBy">
                    <template #body="{ data }">
                      <div class="flex flex-column gap-2">
                        <div>{{ getLastEditTime(data) }}</div>
                        <div>{{ data.lastModifiedBy }}</div>
                      </div>
                    </template>
                  </column>
                  <column header="Статус" field="statusText"> </column>
                  <column header="">
                    <template #body="{ data }">
                      <router-link
                        class="no-underline"
                        :to="{
                          name:
                            data.productionType === 'Product'
                              ? 'edit-product'
                              : data.productionType === 'ServiceProduct'
                              ? 'edit-product-service'
                              : 'edit-product-work',
                          params: { id: data.id },
                        }"
                      >
                        <prime-button-weak
                          class="p-button-rounded edit-button"
                          icon="pi pi-pencil"
                        ></prime-button-weak>
                      </router-link>
                    </template>
                  </column>
                </data-table>
              </div>
              <div v-else class="flex flex-column gap-1">
                <skeleton v-for="i in 15" :key="i" height="70px"></skeleton>
              </div>
              <prime-paginator
                class="mt-2"
                v-if="pageNumber && pageSize && (currentPage?.totalItemCount ?? 0) > 0"
                @page="changePage"
                :rows="pageSize"
                :first="pageSize * (pageNumber - 1)"
                :totalRecords="currentPage?.totalItemCount ?? 0"
              ></prime-paginator>
            </div>
            <div class="col-12"></div>
          </div>
          <template #content> </template>
        </prime-card>
      </tab-panel>
      <tab-panel header="Обмен">
        <excel-tab></excel-tab>
      </tab-panel>
      <tab-panel header="Интеграции">
        <markeplaces-tab></markeplaces-tab>
      </tab-panel>
    </tab-view>
  </app-page>
</template>

<script lang="ts">
import { IproductionsPageRequest } from '@/app/productions/@types/IproductionsPageRequest';
import { ProductionModel } from '@/app/productions/models/production.model';
import { productionsStore } from '@/app/productions/state/productions.store';
import { catalogFiltersStore, ProductionsOrder } from '@/store/catalog-filters.store';
import { PrimePaginator } from '@/tools/prime-vue-components';
import Menu from 'primevue/menu';
import type { MenuItem } from 'primevue/menuitem';
import { computed, defineComponent, ref, watch } from 'vue';

export default defineComponent({
  components: { PrimePaginator, PrimeMenu: Menu },
  setup() {
    const { loadPage, getPageState } = productionsStore;
    const { searchQuery, organization, region, sectionId, showFilters } = catalogFiltersStore;
    const orderBy = ref(ProductionsOrder.IdByDesc);

    const pageNumber = ref(1);
    const pageSize = ref(20);

    const request = computed<IproductionsPageRequest>(() => ({
      name: searchQuery.value,
      pageNumber: pageNumber.value,
      pageSize: pageSize.value,
      producerName: organization.value,
      ProducerId: null,
      region: region.value,
      orderBy: orderBy.value,
      status: null,
      catalogSectionId: sectionId.value,
    }));

    watch(
      request,
      (r) => {
        loadPage(r);
      },
      {
        immediate: true,
        deep: true,
      },
    );

    const pageState = computed(() => getPageState(request.value));

    const pageStatus = computed(() => pageState.value.pageStatus.value);

    const productions = computed(() => pageState.value.pageData.value?.rows);

    const currentPage = computed(() => pageState.value.pageData.value);

    const changePage = ({ page }: { page: number }) => {
      pageNumber.value = page + 1;
    };

    const getLastEditTime = (mod: ProductionModel) =>
      new Date(mod.lastModified).toLocaleDateString();

    const menuItems: MenuItem[] = [
      {
        label: 'Товар',
        to: { name: 'create-product' },
      },
      {
        label: 'Услугу',
        to: { name: 'create-service' },
      },
      {
        label: 'Работу',
        to: { name: 'create-work' },
      },
    ];

    const menu = ref();

    const toggleMenu = (event: PointerEvent) => {
      menu.value.toggle(event);
    };

    return {
      pageStatus,
      pageSize,
      currentPage,
      showFilters,
      productions,
      changePage,
      pageNumber,
      getLastEditTime,
      orderBy,
      menuItems,
      menu,
      toggleMenu,
    };
  },
});
</script>

<style lang="scss" scoped>
.edit-button {
  transform: scale(0.8, 0.8);
}

:deep(.p-column-header-content .p-column-title) {
  font-weight: 400;
  color: #989898;
}

:deep(.p-datatable-thead tr th) {
  background-color: #ffffff;
}

:deep(.p-tabview-panels) {
  padding: 1.25rem 0;
}

:deep(.p-tabview .p-tabview-nav li .p-tabview-nav-link) {
  font-weight: 400;
  color: #000000;
}

:deep(div.p-tabview .p-tabview-panels) {
  background: none !important;
}

:deep(ul.p-tabview-nav) {
  background: none !important;
}

:deep(.p-tabview-header .p-tabview-nav-link.p-tabview-header-action) {
  background: none !important;
}

:deep(.p-tabview .p-tabview-nav li.p-highlight .p-tabview-nav-link) {
  background: #ffffff;
  border-color: #3b82f6;
  color: #3b82f6;
}
</style>
