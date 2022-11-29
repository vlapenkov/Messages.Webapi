<template>
  <div class="text-lg">{{ product.name }}</div>
  <div class="text-sm text-primary">{{ product.country }}</div>
  <div class="text-sm">{{ product.price }} {{ product.currency }}</div>
  <div class="text-sm text-info">{{ product.statusText }}</div>
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
