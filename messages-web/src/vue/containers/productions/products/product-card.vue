<template>
  <card
    @click="viewProduct(product)"
    class="h-full re-padding-card trans-shadow relative"
    :class="{
      'shadow-none': !isCardHovered,
      'shadow-7': isCardHovered,
    }"
    ref="cardRef"
    :style="cardStyle"
  >
    <template #header>
      <div class="w-full h-full" ref="headerRef">
        <file-store-image
          :min-height="206"
          :max-height="206"
          :id="product.documentId"
        ></file-store-image>
      </div>
    </template>
    <template #content>
      <div class="h-full flex flex-column justify-content-between gap-1 p-2">
        <app-price :price="product.price"></app-price>
        <div class="flex flex-grow-1 text-left">
          {{ product.name }}
        </div>
        <div class="h-full flex gap-1 flex-column flex-auto justify-content-end">
          <div class="text-sm text-primary">
            <prime-button
              class="p-button-text text-sm p-0 text-left"
              @click.stop="viewOrganization(product)"
            >
              {{ product.organization.name }}
            </prime-button>
          </div>
          <div class="text-sm">{{ product.organization.region }}</div>
          <div class="text-md">
            <i class="pi pi-star star-yellow"></i> {{ product.rating ?? 0 }}
          </div>
          <div
            :class="{ 'half-transparent': isNotProduct }"
            class="flex flex-row gap-1 align-items-stretch justify-content-between mt-2"
          >
            <prime-button
              :disabled="isNotProduct"
              @click.stop="addToCart(product)"
              class="p-button-sm p-button-outlined h-full py-1 flex-grow-1"
              label="В Корзину"
            >
            </prime-button>
            <prime-button
              :disabled="isNotProduct"
              icon="pi pi-chart-bar"
              class="p-button-secondary p-button-text py-1"
            ></prime-button>
            <prime-button
              :disabled="isNotProduct"
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
import { isAuthenticated } from '@/store/user.store';
import { useElementHover, useElementSize } from '@vueuse/core';
import { computed, CSSProperties, defineComponent, PropType, ref } from 'vue';
import { useRouter } from 'vue-router';

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
  setup(props, { emit }) {
    const router = useRouter();
    const addToCart = (p: ProductionModel) => {
      if (!isAuthenticated.value) {
        router.push({
          name: 'register',
        });
      }
      emit('addToCart', p);
    };
    const viewOrganization = (p: ProductionModel) => {
      emit('viewOrganization', p);
    };
    const viewProduct = (p: ProductionModel) => {
      emit('viewProduct', p);
    };

    const cardRef = ref();
    const isCardHovered = useElementHover(cardRef);

    const headerRef = ref<HTMLElement>();
    const { height: headerHeight } = useElementSize(headerRef);
    const cardStyle = computed<CSSProperties>(() => ({
      '--p-card-body-height': `calc(100% - ${headerHeight.value}px)`,
    }));

    const isNotProduct = computed(() => props.product.productionType !== 'Product');

    return {
      headerRef,
      cardRef,
      cardStyle,
      addToCart,
      viewProduct,
      viewOrganization,
      isCardHovered,
      isNotProduct,
    };
  },
});
</script>

<style lang="scss" scoped>
.star-yellow {
  color: #ffb800;
}
.half-transparent {
  opacity: 0.5;
}
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
