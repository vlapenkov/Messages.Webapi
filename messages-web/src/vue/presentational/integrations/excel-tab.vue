<template>
  <card>
    <template #content>
      <div class="flex card-container blue-container overflow-hidden">
        <div class="flex-none flex align-items-center justify-content-center border-round">
          <img :src="require('@/assets/images/excel.svg')" alt="excel" width="50" :style="{
            objectFit: 'cover',
            borderRadius: '0.5rem',
          }" class="mr-3" />
          <span class="text-xl font-semibold">Загрузка из Exel</span>
        </div>
        <div class="flex-grow-1 flex align-items-center justify-content-center  border-round">
        </div>
        <div class="flex-none flex align-items-center justify-content-center border-round">
          <prime-button icon="pi pi-download" iconPos="right" label="Загрузить"
            @click="vidibleLoginDialog = !vidibleLoginDialog" />
        </div>
      </div>
      <data-table :value="[]" responsiveLayout="scroll">
        <column header="Дата">
          <template #body="slopProps">
            <span class="p-component">
              {{ formatDateString(slopProps.data.lastLoaded) }}
            </span>
          </template>
        </column>
        <column header="Ответственный" field="itemsCount" />
        <column field="Загружено" header="Выгружено заказов" />
      </data-table>
    </template>
  </card>
  <teleport to="body">
    <excel-load-dialog v-model:visible="vidibleLoginDialog" @fileSelected="handleFile" />
  </teleport>
</template>

<script lang="ts">
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { productionsHttpService } from '@/app/productions/infrastructure/productions.http-service';
import { defineComponent, ref } from 'vue';

export default defineComponent({
  setup() {
    const vidibleLoginDialog = ref(false);
    const formatDateString = (d: Date) => {
      const date = new Date(d);
      if (date == null) return '';
      return date.toLocaleString('ru-RU', {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
      });
    };

    const handleFile = async (data: { name: string; b64: string; }) => {
      const b64 = data.b64.replace(/(^data:.*\/.*;base64,)/gi, '');
      console.log(data.name, b64,)
      const response = await productionsHttpService.fromExcel({ fileNmae: data.name, data: b64 })
      if (response.status === HttpStatus.Success) {
        console.log(response);
      } else {
        console.log(response);
      }
    }

    return { formatDateString, handleFile, vidibleLoginDialog };
  },
});
</script>

<style lang="scss" scoped>
:deep(.p-column-header-content .p-column-title) {
  font-weight: 400;
  color: #989898;
}

:deep(.p-datatable-thead tr th) {
  background-color: #FFFFFF;
}

:deep(.p-card-content) {
  padding: 0;
}
</style>
