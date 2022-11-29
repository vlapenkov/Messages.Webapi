<template>
  <div class="text-2xl font-medium mt-1">{{ product.name }}</div>
  <div class="text-base text-primary mt-1">{{ product.organization.name }}</div>
  <div class="text-base mt-1">
    <span class="text-color-secondary">Статус:</span>&nbsp;<span>{{ product.statusText }}</span>
  </div>
  <div class="text-base mt-1">
    <span class="text-color-secondary">Цена:</span>&nbsp;<span>{{ product.price }} ₽</span>
  </div>
  <div class="text-base text-info mt-1">
    <span class="text-color-secondary">Дата актуализации:</span>&nbsp;<span>{{
      product.lastModified
    }}</span>
  </div>
</template>

<script lang="ts">
import { IProductFullModel } from '@/app/product-full/@types/IProductFullModel';
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    product: {
      type: Object as PropType<IProductFullModel>,
      default: () => null,
    },
  },
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
