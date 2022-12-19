<template>
  <app-page title="Товары">
    <card>
      <template #content>
        <data-table :value="productShortsItems" responsiveLayout="scroll">
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
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import { IproductionsPageRequest } from '@/app/productions/@types/IproductionsPageRequest';
import { productionsService } from '@/app/productions/services/productions.service';
import { productionsStore } from '@/app/productions/state/productions.store';
import { IProductStatus, useProductStatuses } from '@/composables/product-statuses.composable';
import { computed, defineComponent, onMounted, Ref, ref, watch } from 'vue';

export default defineComponent({
  setup() {
    onMounted(() => {
      const request: IproductionsPageRequest = {
        name: null,
        catalogSectionId: undefined,
        pageNumber: productionsStore.pageNumber.value,
        pageSize: productionsStore.pageSize.value,
        producerName: null,
        region: null,
      };
      productionsService.loadPage(request);
    });
    const { statuses, initial } = useProductStatuses();
    const productShortsItems = computed(() => productionsStore.currentPageItems.value ?? []);
    const { status: productShortsStatus, pageNumber, pageSize, currentPage } = productionsStore;
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
      productShortsItems.value.forEach((pr) => {
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
      productStatuses,
      productStatusModels,
      statuses,
      productShortsItems,
      changePage,
      productShortsStatus,
      pageSize,
      currentPage,
      changed,
      formatDateString,
    };
  },
});
</script>

<style lang="scss" scoped>
:deep(div.p-dropdown.p-component.p-inputwrapper.p-inputwrapper-filled
    span.p-dropdown-label.p-inputtext) {
  padding: 6px;
}
:deep(.p-card-content) {
  padding: 0;
}
</style>
