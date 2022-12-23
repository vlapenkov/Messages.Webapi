<template>
  <products-list-panel v-bind="{ request }"></products-list-panel>
</template>

<script lang="ts">
import {
  IproductionsPageRequest,
  ProductStatus,
} from '@/app/productions/@types/IproductionsPageRequest';
import { OrderByProduct } from '@/store/catalog-filters.store';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  props: {
    organizationId: {
      type: Number,
      required: true,
    },
  },
  setup(props) {
    const request = computed<IproductionsPageRequest>(() => ({
      name: null,
      catalogSectionId: undefined,
      pageNumber: 1,
      pageSize: 4,
      producerName: null,
      ProducerId: props.organizationId,
      region: null,
      orderBy: OrderByProduct.RatingByDesc,
      status: ProductStatus.Active,
    }));
    return { request };
  },
});
</script>

<style lang="scss"></style>
