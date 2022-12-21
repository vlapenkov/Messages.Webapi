<template>
  <div class="flex flex-row align-items-center gap-3">
    <span class="p-component text-color-secondary">Сортировка:</span>
    <dropdown
      class="pl-1 pb-1"
      :style="{ width: '380px' }"
      :options="ordersByProductWithName"
      optionLabel="name"
      optionValue="value"
      placeholder="Выберите"
      v-model="orderBy"
    />
  </div>
</template>

<script lang="ts">
import { catalogFiltersStore, OrderByProduct } from '@/store/catalog-filters.store';
import { defineComponent } from 'vue';

export interface IOrderByProductWithName {
  value: OrderByProduct;
  name: string;
}

const ordersByProductWithName: IOrderByProductWithName[] = [
  {
    value: OrderByProduct.NameByAsc,
    name: 'по названию (возрастание)',
  },
  {
    value: OrderByProduct.NameByDesc,
    name: 'по названию (убывание)',
  },
  {
    value: OrderByProduct.RegionByAsc,
    name: 'по региону (возрастание)',
  },
  {
    value: OrderByProduct.RegionByDesc,
    name: 'по региону (убывание)',
  },
  {
    value: OrderByProduct.ProducerByAsc,
    name: 'по названию производителя (возрастание)',
  },
  {
    value: OrderByProduct.ProducerByDesc,
    name: 'по названию производителя (убывание)',
  },
  {
    value: OrderByProduct.RatingByAsc,
    name: 'по рейтингу (возрастание)',
  },
  {
    value: OrderByProduct.RatingByDesc,
    name: 'по рейтингу (убывание)',
  },
];

export default defineComponent({
  setup() {
    const { orderBy } = catalogFiltersStore;
    return { ordersByProductWithName, orderBy };
  },
});
</script>

<style lang="scss" scoped>
:deep(span.p-dropdown-label.p-inputtext) {
  padding: 6px;
}
</style>
