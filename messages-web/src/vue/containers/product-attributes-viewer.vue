<template>
  <tag
    v-for="(attr, index) in attrs"
    class="my-2"
    :class="{ 'ml-2': index > 0 }"
    rounded
    :key="attr.attributeId"
    :value="attr.value"
  ></tag>
</template>

<script lang="ts">
import { IProductAttribute } from '@/app/product-full/@types/IProductAttribute';
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    attrs: {
      type: Array as PropType<IProductAttribute[]>,
      default: () => [],
    },
  },
});
</script>

<style scoped></style>
