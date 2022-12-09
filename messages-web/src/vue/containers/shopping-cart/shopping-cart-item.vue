<template>
  <div class="grid">
    <div class="col-2">
      <product-image fit-width :id="item.documentId"></product-image>
    </div>
    <div class="col-4 flex flex-column gap-3">
      <div class="p-component text-md">{{ item.productName }}</div>
      <div class="p-component text-md text-primary">
        <router-link
          :to="{ name: 'organization', params: { id: item.organization.id } }"
          class="flex gap-3 align-items-center not-link"
        >
          {{ item.organization.name }}
        </router-link>
      </div>
      <div class="flex flex-row gap-2">
        <prime-button
          class="text-sm font-normal p-bbutton-sm p-1 p-button-text rk-button"
          icon="pi pi-heart"
          label="Добавить в избранное"
        ></prime-button>
        <prime-button
          class="text-sm font-normal p-bbutton-sm p-1 p-button-text p-button-danger rk-button"
          icon="pi pi-trash"
          label="Удалить"
          @click="deleteItem"
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
import { shoppingCartHttpService } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { ShoppingCartModel } from '@/app/shopping-cart/models/shopping-cart.model';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';

export default defineComponent({
  props: {
    item: {
      type: Object as PropType<ShoppingCartModel>,
      default: null,
    },
  },
  setup(props) {
    const store = shoppingCartStore;

    const isQuantityUpdating = ref(false);

    const quantity = ref(props.item.quantity);
    watch(quantity, async (newVal, prevVal) => {
      if (newVal === prevVal) return;
      isQuantityUpdating.value = true;
      await shoppingCartStore.addToCart({
        productId: props.item.productId,
        quantity: newVal - prevVal,
      });
      if (store.getDataAsync) {
        store.getDataAsync();
      }
      isQuantityUpdating.value = false;
    });
    const updateItemQuantity = (q: number) => {
      quantity.value = q;
    };
    const deleteItem = async () => {
      isQuantityUpdating.value = true;
      const resp = await shoppingCartHttpService.del(props.item);
      if (resp.status === HttpStatus.Success && store.getDataAsync) {
        store.getDataAsync();
        isQuantityUpdating.value = false;
      }
    };
    return {
      quantity,
      isQuantityUpdating,
      updateItemQuantity,
      deleteItem,
    };
  },
});
</script>

<style lang="scss" scoped>
.rk-button {
  :deep(.p-button-label) {
    font-weight: 500;
  }
}

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

.not-link {
  text-decoration: none;
}

a,
a:visited,
a:hover,
a:active {
  color: inherit;
}
</style>
