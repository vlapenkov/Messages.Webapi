<template>
  <app-page title="Товары" class="products-manager-page">
    <div class="w-full flex flex-row justify-content-end align-items-center mb-3">
      <order-by-container v-model="orderBy"></order-by-container>
    </div>
    <card class="shadow-none">
      <template #content>
        <div>
          <div v-if="productionsStatus?.status === 'loaded'">
            <data-table :value="productions" responsiveLayout="scroll">
              <column header="Наименование" headerStyle="width: 30%">
                <template #body="slopProps">
                  <router-link
                    :to="{ name: 'product', params: { id: slopProps.data.id } }"
                    class="no-underline text-color"
                  >
                    <div class="w-full flex flex-row align-items-center">
                      <img
                        v-if="slopProps.data.documentId == null"
                        :src="require('@/assets/images/profile.svg')"
                        alt="Изображение профиля"
                        width="50"
                        height="50"
                        :style="{
                          objectFit: 'cover',
                          borderRadius: '0.5rem',
                        }"
                        class="mr-3"
                      />
                      <file-store-image
                        v-if="slopProps.data.documentId != null"
                        :max-width="50"
                        :max-height="50"
                        :id="slopProps.data.documentId"
                        class="mr-3"
                      ></file-store-image>
                      <span class="p-component">{{ slopProps.data.name }}</span>
                    </div>
                  </router-link>
                </template>
              </column>
              <column header="Наименование организации" headerStyle="width: 25%">
                <template #body="slopProps">
                  <span class="p-component">
                    {{ slopProps.data.organization.name }}
                  </span>
                </template>
              </column>
              <column header="Дата изменения" headerStyle="width: 10%">
                <template #body="slopProps">
                  <span class="p-component">
                    {{ formatDateString(slopProps.data.lastModified) }}
                  </span>
                </template>
              </column>
              <column field="lastModifiedBy" header="Кем изменено" headerStyle="width: 20%" />
              <column field="statusText" header="Статус" headerStyle="width: 15%">
                <template #body="slopProps">
                  <dropdown
                    v-if="productStatusModels != null"
                    v-model="productStatusModels[slopProps.data.id]"
                    :options="statuses"
                    optionLabel="name"
                    class="w-full"
                    :disabled="productStatusModels[slopProps.data.id]?.value !== 0"
                    @change="changed($event, slopProps.data.id)"
                  />
                </template>
              </column>
            </data-table>
          </div>
          <div v-else class="flex flex-column gap-1">
            <skeleton v-for="i in 15" :key="i" height="70px"></skeleton>
          </div>
          <prime-paginator
            class="w-full mt-2"
            v-if="pageNumber && pageSize && (currentPage?.totalItemCount ?? 0) > 0"
            @page="changePage"
            :rows="pageSize"
            :first="pageSize * (pageNumber - 1)"
            :totalRecords="currentPage?.totalItemCount ?? 0"
          ></prime-paginator>
        </div>
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import { IproductionsPageRequest } from '@/app/productions/@types/IproductionsPageRequest';
import { productionsService } from '@/app/productions/services/productions.service';
import { productionsStore } from '@/app/productions/state/productions.store';
import { IProductStatus, useProductStatuses } from '@/composables/product-statuses.composable';
import { catalogFiltersStore, ProductionsOrder } from '@/store/catalog-filters.store';
import { PrimePaginator } from '@/tools/prime-vue-components';
import { computed, defineComponent, Ref, ref, watch } from 'vue';

export default defineComponent({
  components: { PrimePaginator },
  setup() {
    const pageNumber = ref(1);
    const pageSize = ref(20);
    const { loadPage, getPageState } = productionsStore;
    const { searchQuery, region, organization, sectionId } = catalogFiltersStore;

    const orderBy = ref(ProductionsOrder.IdByDesc);

    const request = computed<IproductionsPageRequest>(() => ({
      name: searchQuery.value ?? null,
      pageNumber: pageNumber.value,
      pageSize: pageNumber.value,
      producerName: organization.value,
      ProducerId: null,
      catalogSectionId: sectionId.value,
      region: region.value,
      orderBy: orderBy.value,
      status: null,
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

    const currentPageState = computed(() => getPageState(request.value));

    const productions = computed(() => currentPageState.value.pageData.value?.rows);

    const productionsStatus = computed(() => currentPageState.value.pageStatus.value);

    const currentPage = computed(() => currentPageState.value.pageData.value);

    const { statuses, initial } = useProductStatuses();
    const formatDateString = (d: Date) => {
      const date = new Date(d);
      if (date == null) return '';
      return date.toLocaleString('ru-RU', {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
      });
    };
    const productStatusModels = ref<Record<number, IProductStatus | undefined>>();
    const productStatuses = computed<Record<number, IProductStatus | undefined>>(() => {
      const res: Record<number, IProductStatus | undefined> = {};
      if (initial.value == null) return res;
      (productions.value ?? []).forEach((pr) => {
        Object.assign(res, {
          ...res,
          [pr.id]: statuses.value.find((s) => s.name === pr.statusText) ?? initial.value,
        });
      });
      return res;
    });
    watch(
      productStatuses,
      (val) => {
        productStatusModels.value = val;
      },
      {
        immediate: true,
      },
    );
    const changed = async (val: Ref<IProductStatus>, id: number) => {
      const status = val.value.value;
      await productionsService.updateStatus(id, status);
    };
    const changePage = ({ page }: { page: number }) => {
      pageNumber.value = page + 1;
    };
    return {
      statuses,
      productStatuses,
      orderBy,
      productStatusModels,
      productionsStatus,
      productions,
      currentPage,
      pageNumber,
      pageSize,
      changed,
      changePage,
      formatDateString,
    };
  },
});
</script>

<style lang="scss" scoped>
.products-manager-page {
  :deep(.p-card-content) {
    padding: 0;
  }
}
</style>
