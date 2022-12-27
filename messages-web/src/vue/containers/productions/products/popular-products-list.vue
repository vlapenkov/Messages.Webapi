<template>
  <products-list-panel v-bind="{ request }"></products-list-panel>
</template>

<script lang="ts">
import {
  IproductionsPageRequest,
  ProductStatus,
} from '@/app/productions/@types/IproductionsPageRequest';
import { ProductionsOrder } from '@/store/catalog-filters.store';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const request: IproductionsPageRequest = {
      name: null,
      catalogSectionId: null,
      pageNumber: 1,
      pageSize: 12,
      producerName: null,
      ProducerId: null,
      region: null,
      orderBy: ProductionsOrder.RatingByDesc,
      status: ProductStatus.Active,
    };
    return { request };
  },
});
</script>

<style lang="scss"></style>
