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
import { ordersByProductWithName } from '@/app/productions/models/OrderByProduct';
import { catalogFiltersStore } from '@/store/catalog-filters.store';
import { defineComponent } from 'vue';

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
