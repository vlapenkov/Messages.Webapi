<template>
  <card class="h-full re-padding-card" :style="cardStyle">
    <template #header>
      <div class="w-full h-full" ref="headerRef">
        <product-image :min-height="206" :max-height="206" :id="product.documentId"></product-image>
      </div>
    </template>
    <template #content>
      <div class="h-full flex flex-column justify-content-between p-2">
        <div class="flex flex-grow-1">
          <prime-button class="p-button-text text-sm font-bold p-0" @click="viewProduct(product)">
            {{ product.name }}
          </prime-button>
        </div>
        <div class="h-full flex flex-column flex-auto justify-content-end">
          <div class="text-sm text-primary">
            <prime-button class="p-button-text text-sm p-0" @click="viewOrganization(product)">
              {{ product.organization.name }}
            </prime-button>
          </div>
          <div class="text-sm">{{ product.organization.region }}</div>
          <div class="flex flex-row gap-1 align-items-stretch justify-content-between mt-2">
            <prime-button
              @click="addToCart(product)"
              class="p-button-sm h-full py-1 flex-grow-1"
              label="В Корзину"
            >
            </prime-button>
            <prime-button
              disabled
              icon="pi pi-heart"
              class="p-button-secondary p-button-text py-1"
            ></prime-button>
            <prime-button
              disabled
              icon="pi pi-chart-bar"
              class="p-button-secondary p-button-text py-1"
            ></prime-button>
            <prime-button
              disabled
              icon="pi pi-arrows-h"
              class="p-button-secondary p-button-text py-1"
            ></prime-button>
          </div>
        </div>
      </div>
    </template>
  </card>
</template>

<script lang="ts">
import { ProductionModel } from '@/app/productions/models/production.model';
import { useElementSize } from '@vueuse/core';
import { computed, CSSProperties, defineComponent, PropType, ref } from 'vue';

export default defineComponent({
  props: {
    product: {
      type: Object as PropType<ProductionModel>,
      default: null,
    },
  },
  emits: {
    addToCart: (_: ProductionModel) => true,
    viewProduct: (_: ProductionModel) => true,
    viewOrganization: (_: ProductionModel) => true,
  },
  setup(_, { emit }) {
    const addToCart = (p: ProductionModel) => {
      emit('addToCart', p);
    };
    const viewOrganization = (p: ProductionModel) => {
      emit('viewOrganization', p);
    };
    const viewProduct = (p: ProductionModel) => {
      emit('viewProduct', p);
    };

    const headerRef = ref<HTMLElement>();
    const { height: headerHeight } = useElementSize(headerRef);
    const cardStyle = computed<CSSProperties>(() => ({
      '--p-card-body-height': `calc(100% - ${headerHeight.value}px)`,
    }));

    return {
      headerRef,
      cardStyle,
      addToCart,
      viewProduct,
      viewOrganization,
    };
  },
});
</script>

<style lang="scss" scoped>
.re-padding-card {
  :deep(.p-card-header) {
    line-height: 0;
    text-align: center;
  }

  :deep(.p-card-body) {
    padding: 0;
    height: var(--p-card-body-height) !important;
  }

  :deep(.p-card-content) {
    padding-bottom: 0;
    padding-top: 0;
    height: 100% !important;
  }
}
</style>
