<template>
  <card class="h-full re-padding-card" :style="cardStyle">
    <template #header>
      <div class="w-full h-full" ref="headerRef">
        <product-image
          :min-height="140"
          object-fit="cover"
          :id="product.documentId"
        ></product-image>
      </div>
    </template>
    <template #content>
      <div class="h-full flex flex-column justify-content-between p-2">
        <div class="flex flex-grow-1">
          <prime-button class="p-button-text text-sm font-bold p-0" @click="viewProduct(product)">
            {{ product.name }}
          </prime-button>
        </div>
        <div class="h-full flex flex-shrink-1 flex-column flex-auto justify-content-end">
          <div class="text-sm text-primary">
            <prime-button class="p-button-text text-sm p-0" @click="viewOrganization(product)">
              {{ product.organization.name }}
            </prime-button>
          </div>
          <div class="text-sm">{{ product.organization.region }}</div>
          <div class="flex flex-row gap-1 align-items-stretch justify-content-between mt-2">
            <span>
              <prime-button
                @click="addToCart(product)"
                class="p-button-sm h-full py-1"
                label="Заказать"
              >
              </prime-button>
            </span>
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
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { useElementSize } from '@vueuse/core';
import { computed, CSSProperties, defineComponent, PropType, ref } from 'vue';

export default defineComponent({
  props: {
    product: {
      type: Object as PropType<ProductShortModel>,
      default: null,
    },
  },
  emits: {
    addToCart: (_: ProductShortModel) => true,
    viewProduct: (_: ProductShortModel) => true,
    viewOrganization: (_: ProductShortModel) => true,
  },
  setup(_, { emit }) {
    const addToCart = (p: ProductShortModel) => {
      emit('addToCart', p);
    };
    const viewOrganization = (p: ProductShortModel) => {
      emit('viewOrganization', p);
    };
    const viewProduct = (p: ProductShortModel) => {
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
