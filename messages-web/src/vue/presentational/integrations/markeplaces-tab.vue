<template>
  <card>
    <template #content>
      <data-table :value="items" responsiveLayout="scroll">
        <column header="Торговая площадка" headerStyle="width: 30%">
          <template #body="slopProps">
            <div class="w-full flex flex-row align-items-center">
              <img :src="require('@/assets' + slopProps.data.image)" alt="Изображение профиля" width="100" :style="{
                objectFit: 'cover',
                borderRadius: '0.5rem',
              }" class="mr-3" />
              <span class="p-component">{{ slopProps.data.name }}</span>
            </div>
          </template>
        </column>
        <column header="Выгружено товаров" field="itemsCount" />
        <column header="Дата выгрузки товаров">
          <template #body="slopProps">
            <span class="p-component">
              {{ formatDateString(slopProps.data.lastUploaded) }}
            </span>
          </template>
        </column>
        <column field="ordersCount" header="Загружено заказов" />
        <column header="Дата загрузки заказов">
          <template #body="slopProps">
            <span class="p-component">
              {{ formatDateString(slopProps.data.lastLoaded) }}
            </span>
          </template>
        </column>
        <column field="statusText" header="Статус">
          <template #body="slopProps">
            <checkbox inputId="binary" v-model="slopProps.data.active" :binary="true" :disabled="true" />
          </template>
        </column>
      </data-table>
    </template>
  </card>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const formatDateString = (d: Date) => {
      const date = new Date(d);
      if (date == null) return '';
      return date.toLocaleString('ru-RU', {
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric'
      });
    };

    const items: {
      image: string;
      name: string;
      itemsCount: number;
      lastUploaded: Date;
      lastLoaded: Date;
      ordersCount: number;
      active: boolean;
    }[] = [
        { image: '/images/ozon.svg', name: "Ozon", itemsCount: 234, lastLoaded: new Date('2022-12-13T13:12:00Z'), ordersCount: 43, lastUploaded: new Date('2022-12-13T13:49:00Z'), active: true },
        { image: '/images/yandex.svg', name: "Яндекс Маркет", itemsCount: 12, lastLoaded: new Date('2022-12-14T12:43:00Z'), ordersCount: 4, lastUploaded: new Date('2022-12-15T13:54:00Z'), active: true },
        { image: '/images/avito.svg', name: "Avito", itemsCount: 76, lastLoaded: new Date('2022-12-16T15:00:00Z'), ordersCount: 18, lastUploaded: new Date('2022-12-19T14:12:00Z'), active: false }
      ];
    return { items, formatDateString };
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
