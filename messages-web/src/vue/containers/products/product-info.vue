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
      const datePart = props.product.lastModified?.split('T')[0];
      const timePart = props.product.lastModified?.split('T')[1]?.split('.')[0];
      const datePartSplited = datePart.split('-');
      const timePartSplited = timePart.split(':');
      dateStr = `${datePartSplited[2]}.${datePartSplited[1]}.${datePartSplited[0]} ${timePartSplited[0]}:${timePartSplited[1]}:${timePartSplited[2]}`;
    }
    return { dateStr };
  },
});
</script>

<style scoped></style>
