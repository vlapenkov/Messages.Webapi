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
import { ProductionsOrder } from '@/store/catalog-filters.store';
import { computed, defineComponent, PropType } from 'vue';

export interface IProductionsOrderWithName {
  value: ProductionsOrder;
  name: string;
}

const ordersByProductWithName: IProductionsOrderWithName[] = [
  {
    value: ProductionsOrder.NameByAsc,
    name: 'по названию (возрастание)',
  },
  {
    value: ProductionsOrder.NameByDesc,
    name: 'по названию (убывание)',
  },
  {
    value: ProductionsOrder.RegionByAsc,
    name: 'по региону (возрастание)',
  },
  {
    value: ProductionsOrder.RegionByDesc,
    name: 'по региону (убывание)',
  },
  {
    value: ProductionsOrder.ProducerByAsc,
    name: 'по названию производителя (возрастание)',
  },
  {
    value: ProductionsOrder.ProducerByDesc,
    name: 'по названию производителя (убывание)',
  },
  {
    value: ProductionsOrder.RatingByAsc,
    name: 'по рейтингу (возрастание)',
  },
  {
    value: ProductionsOrder.RatingByDesc,
    name: 'по рейтингу (убывание)',
  },
  {
    value: ProductionsOrder.IdByAsc,
    name: 'по дате (от старых к новым)',
  },
  {
    value: ProductionsOrder.IdByDesc,
    name: 'по дате (от новых к старым)',
  },
];

export default defineComponent({
  props: {
    modelValue: {
      type: Number as PropType<ProductionsOrder>,
      default: null,
    },
  },
  emits: {
    'update:modelValue': (_: ProductionsOrder | null) => true,
  },
  setup(props, { emit }) {
    const orderBy = computed({
      get: () => props.modelValue,
      set: (val) => {
        emit('update:modelValue', val);
      },
    });
    return { ordersByProductWithName, orderBy };
  },
});
</script>

<style lang="scss" scoped>
:deep(span.p-dropdown-label.p-inputtext) {
  padding: 6px;
}
</style>
