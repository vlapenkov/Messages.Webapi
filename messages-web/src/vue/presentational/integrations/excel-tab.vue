<template>
  <card class="shadow-1">
    <template #content>
      <div class="flex card-container blue-container overflow-hidden">
        <div class="flex-none flex align-items-center justify-content-center border-round">
          <img
            :src="require('@/assets/images/excel.svg')"
            alt="excel"
            width="50"
            :style="{
              objectFit: 'cover',
              borderRadius: '0.5rem',
            }"
            class="mr-3"
          />
          <span class="text-xl font-semibold">Загрузка из Excel</span>
        </div>
        <div class="flex-grow-1 flex align-items-center justify-content-center border-round"></div>
        <div class="flex-none flex align-items-center justify-content-center border-round">
          <prime-button
            icon="pi pi-download"
            iconPos="right"
            label="Загрузить"
            @click="vidibleselectDialog = !vidibleselectDialog"
          />
        </div>
      </div>
      <data-table :value="exchangesList" responsiveLayout="scroll">
        <column field="productExchangeText" header="Вид" headerStyle="width: 25%" />
        <column header="Дата" headerStyle="width: 25%">
          <template #body="slopProps">
            <span class="p-component">
              {{ formatDateString(slopProps.data.lastModified) }}
            </span>
          </template>
        </column>
        <column field="createdBy" header="Ответственный" headerStyle="width: 25%" />
        <column field="productsLoaded" header="Загружено товаров" headerStyle="width: 25%" />
      </data-table>
    </template>
  </card>
  <teleport to="body">
    <toast position="top-right" group="tr1" />
    <excel-load-dialog v-model:visible="vidibleselectDialog" @fileSelected="handleFile" />
    <excel-loading-indicator v-model:visible="vidibleLoadingDialog" />
  </teleport>
</template>

<script lang="ts">
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { productFullHttpService } from '@/app/product-full/infrastructure/product-full.http-service';
import { exchangeService } from '@/app/productions/services/exchange.service';
import { productionsService } from '@/app/productions/services/productions.service';
import { exchangesStore } from '@/app/productions/state/exchanges.store';
import { productionsStore } from '@/app/productions/state/productions.store';
import { catalogFiltersStore } from '@/store/catalog-filters.store';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { defineComponent, onMounted, ref } from 'vue';

export default defineComponent({
  components: { Toast },
  setup() {
    onMounted(async () => {
      await exchangeService.load();
    });
    const { list: exchangesList, status: exchangesStatus } = exchangesStore;
    const toast = useToast();
    const vidibleselectDialog = ref(false);
    const vidibleLoadingDialog = ref(false);
    const formatDateString = (d: Date) => {
      const date = new Date(d);
      if (date == null) return '';
      return date.toLocaleString('ru-RU');
    };
    const { pageNumber, pageSize } = productionsStore;
    const { searchQuery, orderBy } = catalogFiltersStore;
    const handleFile = async (data: { name: string; b64: string }) => {
      const b64 = data.b64.replace(/(^data:.*\/.*;base64,)/gi, '');
      // console.log(data.name, b64,);
      try {
        vidibleLoadingDialog.value = true;
        const response = await productFullHttpService.fromExcel({ fileName: data.name, data: b64 });
        // console.log(response);
        vidibleLoadingDialog.value = false;
        if (response.status === HttpStatus.Success) {
          toast.add({
            severity: 'success',
            group: 'tr1',
            summary: 'Успех',
            detail: 'Загрузка из Excel завершилась уcпешно',
            life: 4000,
          });
          await exchangeService.load();
          productionsService.loadPage({
            name: searchQuery.value ?? null,
            pageNumber: pageNumber.value,
            pageSize: pageSize.value,
            producerName: null,
            region: null,
            orderBy: orderBy.value,
            status: null,
          });
        } else {
          toast.add({
            severity: 'error',
            group: 'tr1',
            summary: 'Ошибка',
            detail: 'Загрузка из Excel завершилось с ошибкой',
            life: 4000,
          });
        }
      } catch (err) {
        // console.error(err)
        vidibleLoadingDialog.value = false;
        toast.add({
          severity: 'error',
          group: 'tr1',
          summary: 'Ошибка',
          detail: 'Загрузка из Excel завершилось с ошибкой',
          life: 4000,
        });
      }
    };
    return {
      vidibleselectDialog,
      vidibleLoadingDialog,
      exchangesList,
      exchangesStatus,
      formatDateString,
      handleFile,
    };
  },
});
</script>

<style lang="scss" scoped>
:deep(.p-column-header-content .p-column-title) {
  font-weight: 400;
  color: #989898;
}

:deep(.p-datatable-thead tr th) {
  background-color: #ffffff;
}

:deep(.p-card-content) {
  padding: 0;
}
</style>
