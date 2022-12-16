<template>
  <card
    @click="viewProduct(product)"
    class="h-full re-padding-card trans-shadow relative"
    :class="{
      'shadow-none': !isCardHovered,
      'shadow-2': isCardHovered,
    }"
    ref="cardRef"
    :style="cardStyle"
  >
    <template #header>
      <div class="relative" ref="headerRef">
        <file-store-image
          fit-width
          :min-height="206"
          :max-height="206"
          :id="product.documentId"
        ></file-store-image>
        <div v-if="productType" class="absolute left-0 bottom-0 p-2">
          <tag severity="warning" v-if="product.availableStatusText" class="tag-height" rounded>
            <div class="text-sm font-normal lowercase">{{ product.availableStatusText }}</div>
          </tag>
        </div>
        <div v-if="productType" class="absolute right-0 bottom-0 p-2">
          <tag class="tag-secondary tag-height lowercase" rounded>
            <div class="text-sm font-normal">{{ productType }}</div>
          </tag>
        </div>
      </div>
    </template>
    <template #content>
      <div class="h-full flex flex-column justify-content-between gap-1 p-2">
        <div class="text-sm font-normal article">
          {{ product.codeTnVed || '123456' }}
        </div>
        <app-price :price="product.price"></app-price>
        <div class="flex flex-grow-1 name-font">
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
          <div class="text-sm article">{{ product.organization.region }}</div>
          <div
            class="flex flex-row gap-1 align-content-center text-md"
            :style="{ opacity: (product.rating ?? 0) > 0 ? 1 : 0 }"
          >
            <i class="star-filled star-yellow"></i>
            <span>
              {{ product.rating ?? 0 }}
            </span>
          </div>
          <div
            :class="{ 'half-transparent': isNotProduct }"
            class="flex flex-row gap-1 align-items-stretch justify-content-between mt-2"
          >
            <prime-button
              :disabled="isNotProduct"
              @click.stop="addToCart(product)"
              class="p-button-sm p-button-outlined h-full py-1 flex-grow-1"
            >
              <span class="font-medium w-full">В корзину</span>
            </prime-button>
            <prime-button
              disabled
              icon="pi pi-chart-bar"
              class="p-button-secondary p-button-outlined p-0 py-1 square-button"
            ></prime-button>
            <prime-button
              disabled
              icon="pi pi-arrows-h"
              class="p-button-secondary p-button-outlined p-0 py-1 square-button"
            ></prime-button>
          </div>
        </div>
        <teleport to="body">
          <login-register-dialog :visible="visibleModel" @update:visible="updatevisibleModel" />
        </teleport>
      </div>
    </template>
  </card>
</template>

<script lang="ts">
import { ProductionModel } from '@/app/productions/models/production.model';
import { isAuthenticated } from '@/store/user.store';
import { useElementHover, useElementSize } from '@vueuse/core';
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
  setup(props, { emit }) {
    const visibleModel = ref(false);
    const updatevisibleModel = (v: boolean) => {
      visibleModel.value = v;
    };
    const addToCart = (p: ProductionModel) => {
      visibleModel.value = !isAuthenticated.value;
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

    const productType = computed(() => {
      switch (props.product.productionType) {
        case 'Product':
          return 'Товар';
        case 'WorkProduct':
          return 'Работа';
        case 'ServiceProduct':
          return 'Услуга';
        default:
          return null;
      }
    });

    return {
      headerRef,
      cardRef,
      cardStyle,
      addToCart,
      viewProduct,
      viewOrganization,
      isCardHovered,
      isNotProduct,
      visibleModel,
      updatevisibleModel,
      productType,
    };
  },
});
</script>

<style lang="scss" scoped>
.article {
  font-style: normal;
  font-weight: 400;
  font-size: 12px;
  line-height: 14px;
  color: #898989;
}

.name-font {
  font-style: normal;
  font-weight: 400;
  font-size: 16px;
  line-height: 19px;

  color: #3d3d3d;
}

.square-button {
  flex-basis: 33px;
}

.tag-secondary {
  background-color: rgba(180, 187, 186, 0.39);
}

.tag-height {
  min-height: 24px;
}

.star-filled {
  &::before {
    content: url('@/assets/icons/star.svg');
  }
}
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
