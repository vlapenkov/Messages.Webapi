<template>
  <div class="grid">
    <div class="col-2">
      <product-image fit-width :id="item.documentId"></product-image>
    </div>
    <div class="col-4 flex flex-column gap-3">
      <div class="p-component text-md">{{ item.productName }}</div>
      <div class="p-component text-md text-primary">{{ item.organization.name }}</div>
      <div class="flex flex-row gap-2">
        <prime-button
          class="text-sm font-normal p-bbutton-sm p-1 p-button-text"
          icon="pi pi-heart"
          label="Добавить в избранное"
          disabled
        ></prime-button>
        <prime-button
          class="text-sm font-normal tex p-bbutton-sm p-1 p-button-text p-button-danger"
          icon="pi pi-trash"
          label="Удалить"
          disabled
        ></prime-button>
      </div>
    </div>
    <div class="col-4 flex flex-column gap-3">
      <div class="p-component text-md">Цена: {{ item.price }} ₽</div>
    </div>
    <div class="col-2">
      <input-number
        inputId="horizontal"
        :modelValue="quantity"
        :min="1"
        @update:modelValue="updateItemQuantity"
        class="re-scale"
        :disabled="isQuantityUpdating"
        showButtons
        buttonLayout="horizontal"
        decrementButtonClass="p-button-secondary"
        incrementButtonClass="p-button-secondary"
        incrementButtonIcon="pi pi-plus"
        decrementButtonIcon="pi pi-minus"
      />
    </div>
    <div class="col-12">
      <prime-divider class="mb-2 mt-0"></prime-divider>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType, ref, watch } from 'vue';
import { addToCart } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { ShoppingCartModel } from '@/app/shopping-cart/models/shopping-cart.model';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { CollectionStoreMixed } from '../base/presentational/state/collection/collection-state.vue';
import { refreshProvider } from '../base/presentational/state/collection/providers/refresh.provider';

export default defineComponent({
  props: {
    item: {
      type: Object as PropType<ShoppingCartModel>,
      default: null,
    },
  },
  setup(props) {
    const state = shoppingCartStore as CollectionStoreMixed;
    refreshProvider.provideFrom(() => state.getDataAsync);

    const isQuantityUpdating = ref(false);

    const quantity = ref(props.item.quantity);
    watch(quantity, async (newVal, prevVal) => {
      if (newVal === prevVal) return;
      isQuantityUpdating.value = true;
      await addToCart({
        productId: props.item.productId,
        quantity: newVal - prevVal,
      });
      if (state.getDataAsync) {
        state.getDataAsync({ force: true });
      }
      isQuantityUpdating.value = false;
    });
    const updateItemQuantity = (q: number) => {
      quantity.value = q;
    };
    return {
      quantity,
      shoppingCartStore,
      isQuantityUpdating,
      updateItemQuantity,
    };
  },
});
</script>

<style lang="scss" scoped>
.re-scale {
  :deep(.p-inputnumber-input) {
    min-width: 50px;
    max-width: 55px;
    border: 0;
    background-color: var(--surface-200);

    &:focus {
      outline: none;
      box-shadow: none;
    }
  }
  :deep(.p-button) {
    border: 0;
    background-color: var(--surface-200);
    color: var(--text-color);
  }
  :deep(input) {
    border: 0;
    font-size: 28px;
    padding-left: 1rem;
  }
  transform: scale(0.5) translate(-50%, -50%);
}
</style>
