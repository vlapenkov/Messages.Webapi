<template>
  <prime-card
    image-header
    transparent
    no-padding
    @click="viewProduct(product)"
    class="h-full relative"
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
        <prime-button
          style="box-shadow: none"
          @click.stop="isInFavorites = !isInFavorites"
          class="absolute p-button-text p-1 right-0 top-0 m-2"
        >
          <i class="heart" :class="{ invert: isInFavorites }"></i>
        </prime-button>
        <div class="absolute left-0 bottom-0 p-2">
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
        <div
          class="text-sm font-normal article"
          :class="{ 'opacity-0': product.article == null || product.article === '' }"
        >
          {{ product.article || '' }}
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
          <div class="flex flex-row gap-1 align-items-stretch justify-content-between mt-2">
            <prime-button
              :disabled="isNotProduct"
              @click.stop="addToCart(product)"
              :class="{ 'p-button-outlined': !isInCart }"
              class="p-button-sm h-full py-1 flex-grow-1"
            >
              <div class="flex flex-row w-full gap-3 align-items-center justify-content-center">
                <i v-if="isInCart" class="pi pi-check"></i>
                <span class="font-medium">
                  <template v-if="isInCart"> В корзине</template>
                  <template v-else>В корзину</template>
                </span>
              </div>
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
      </div>
    </template>
  </prime-card>
</template>

<script lang="ts">
import { ProductionModel } from '@/app/productions/models/production.model';
import { useIsInCart } from '@/composables/shopping-cart.composables';
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
  setup(props, { emit }) {
    const isInCart = useIsInCart(props.product.id);

    const addToCart = (p: ProductionModel) => {
      if (isInCart.value) {
        return;
      }

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

    const isInFavorites = ref(false);

    return {
      headerRef,
      cardStyle,
      addToCart,
      viewProduct,
      viewOrganization,
      isNotProduct,
      productType,
      isInFavorites,
      isInCart,
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

.heart {
  &:not(.invert) {
    &:not(:hover) {
      &::before {
        content: url('@/assets/icons/heart.svg');
      }
    }
    &:hover {
      &::before {
        content: url('@/assets/icons/heart-filled.svg');
      }
    }
  }
  &.invert {
    &::before {
      content: url('@/assets/icons/heart-filled.svg');
    }
  }
  &:hover {
    transform: scale(1.3, 1.3);
  }
  transition: transform 0.3s;
}

:deep(.p-card-body) {
  height: var(--p-card-body-height) !important;
}
</style>
