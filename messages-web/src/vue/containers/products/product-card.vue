<template>
  <card class="h-full re-padding-card">
    <template #header>
      <product-image :max-height="140" :id="product.documentId"></product-image>
    </template>
    <template #content>
      <div class="p-2 flex flex-column justify-content-between gap-1">
        <div class="text-sm font-bold">{{ product.name }}</div>
        <div class="text-sm text-primary">{{ product.organization.name }}</div>
        <div class="text-sm">{{ product.organization.region }}</div>
        <div class="flex flex-row gap-1 align-items-stretch justify-content-between mt-2">
          <span>
            <prime-button
              @click="addToCart(product)"
              class="p-button-sm h-full py-1"
              label="заказать"
            >
            </prime-button>
          </span>

          <prime-button disabled icon="pi pi-heart" class="p-button-secondary py-1"></prime-button>
          <prime-button
            disabled
            icon="pi pi-chart-bar"
            class="p-button-secondary py-1"
          ></prime-button>
          <prime-button
            disabled
            icon="pi pi-arrows-h"
            class="p-button-secondary py-1"
          ></prime-button>
        </div>
      </div>
    </template>
  </card>
</template>

<script lang="ts">
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    product: {
      type: Object as PropType<ProductShortModel>,
      default: null,
    },
  },
  emits: {
    addToCart: (_: ProductShortModel) => true,
  },
  setup(props, { emit }) {
    const addToCart = (p: ProductShortModel) => {
      emit('addToCart', p);
    };
    return {
      addToCart,
    };
  },
});
</script>

<style lang="scss">
.re-padding-card {
  :deep(.p-card-header) {
    line-height: 0;
    text-align: center;
  }

  .p-card-header {
    line-height: 0 !important;
    text-align: center !important;
  }

  :deep(.p-card-body) {
    padding: 0;
  }

  .p-card-body {
    padding: 0 !important;
  }

  :deep(.p-card-content) {
    padding-bottom: 0;
    padding-top: 0;
  }

  .p-card-content {
    padding-bottom: 0 !important;
    padding-top: 0 !important;
  }
}
</style>
