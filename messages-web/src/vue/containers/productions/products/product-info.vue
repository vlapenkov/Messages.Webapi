<template>
  <div class="text-2xl font-medium mt-1">{{ product.name }}</div>
  <div class="text-base text-primary mt-1">
    <router-link
      :to="{ name: 'organization', params: { id: product.organization.id } }"
      class="flex gap-3 align-items-center not-link"
    >
      {{ product.organization.name }}</router-link
    >
  </div>
  <div class="text-base mt-1">
    <span class="text-color-secondary">Статус:</span>&nbsp;<span>{{ product.statusText }}</span>
  </div>
  <div class="text-base mt-1">
    <span class="text-color-secondary">Цена:</span>&nbsp;<span>{{ product.price }} ₽</span>
  </div>
  <div class="text-base text-info mt-1">
    <span class="text-color-secondary">Дата актуализации:</span>&nbsp;<span>{{ dateStr }}</span>
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
  setup(props) {
    let dateStr = '';
    if (props?.product?.lastModified != null) {
      const d = new Date(props?.product?.lastModified);
      dateStr = d.toLocaleString();
    }
    return { dateStr };
  },
});
</script>

<style scoped>
.not-link {
  text-decoration: none;
  /* color: black; */
}

a,
a:visited,
a:hover,
a:active {
  color: inherit;
}
</style>
