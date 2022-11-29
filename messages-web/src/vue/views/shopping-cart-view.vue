<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <app-page title="Корзина">
    <div class="flex flex-column">
      <card class="shopping-cart-inner-card">
        <template #content>
          <div v-for="item in items" :key="item.productId" class="grid">
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
                :modelValue="item.quantity"
                :min="1"
                @update:modelValue="($event:number) => updateItemQuantity(item, $event)"
                class="re-scale"
                :disabled="isQuantityUpdating(item.productId)"
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
          <div class="flex flex-row justify-content-between align-items-center">
            <div class="p-component text-lg font-semibold">Общая стоимость: {{ sum }} ₽</div>
            <prime-button @click="createNewOrder" label="Перейти к оформлению"></prime-button>
          </div>
        </template>
      </card>
    </div>
  </app-page>
</template>

<script lang="ts">
import {
  addToCart,
  createOrder,
} from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { ShoppingCartModel } from '@/app/shopping-cart/models/shopping-cart.model';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { computed, defineComponent, ref, WritableComputedRef } from 'vue';
import { useRouter } from 'vue-router';
import { CollectionStoreMixed } from '../base/presentational/state/collection/collection-state.vue';
import { createItemProvider } from '../base/presentational/state/collection/providers/create-item.provider';
import { deleteItemProvider } from '../base/presentational/state/collection/providers/delete-item.provider';
import { editOrCreateModeProvider } from '../base/presentational/state/collection/providers/edit-or-create-mode.provider';
import { itemSelectedProvider } from '../base/presentational/state/collection/providers/item-selected.provider';
import { loadingStatusProvider } from '../base/presentational/state/collection/providers/loading-status.provider';
import { refreshProvider } from '../base/presentational/state/collection/providers/refresh.provider';
import { reloadOnSaveProvider } from '../base/presentational/state/collection/providers/reload-on-save.provider';
import { saveChangesProvider } from '../base/presentational/state/collection/providers/save-changes.provider';
import { selectItemProvider } from '../base/presentational/state/collection/providers/select-item.provider';
import { showDialogProvider } from '../base/presentational/state/collection/providers/show-dialog.provider';

export default defineComponent({
  setup() {
    const state = shoppingCartStore as CollectionStoreMixed;
    refreshProvider.provideFrom(() => state.getDataAsync);
    showDialogProvider.provide();
    selectItemProvider.provideFrom(() => state.selectItem);
    createItemProvider.provideFrom(() => state.createItem);
    saveChangesProvider.provideFrom(() => state.saveChanges);
    deleteItemProvider.provideFrom(() => state.deleteItem);
    itemSelectedProvider.provideFrom(() => state.itemSelected);
    editOrCreateModeProvider.provideFrom(() => state.itemSelected?.value?.mode);
    loadingStatusProvider.provideFrom(() => state.status);
    reloadOnSaveProvider.provide(ref(true));
    if (state.items == null) {
      throw new Error('Что-то пошло не так');
    }
    const items = state.items({ force: true }) as WritableComputedRef<ShoppingCartModel[]>;
    const router = useRouter();
    const create = async () => {
      await createOrder();
      router.push({ name: 'orders' });
    };

    const disabledProducts = ref<number[]>([]);

    const updateItemQuantity = async (
      { productId, quantity: quantityOld }: ShoppingCartModel,
      quantity: number,
    ) => {
      disabledProducts.value.push(productId);
      await addToCart({
        productId,
        quantity: quantity - quantityOld,
      });
      if (state.getDataAsync) {
        state.getDataAsync({ force: true });
      }
      disabledProducts.value = disabledProducts.value.filter((el) => el !== productId);
    };
    const isQuantityUpdating = (id: number) =>
      disabledProducts.value.find((el) => el === id) != null;
    const createNewOrder = async () => {
      await createOrder();
    };

    const sum = computed(() =>
      (items.value ?? []).map((i) => i.price * i.quantity).reduce((acc, curr) => acc + curr, 0),
    );
    return {
      shoppingCartStore,
      create,
      items,
      updateItemQuantity,
      isQuantityUpdating,
      createNewOrder,
      sum,
    };
  },
});
</script>

<style lang="scss" scoped>
.re-scale {
  :deep(.p-inputnumber-input) {
    min-width: 50px;
    max-width: 55px;
    background-color: var(--surface-200);
  }
  :deep(.p-button) {
    background-color: var(--surface-200);
    color: var(--text-color);
  }
  :deep(input) {
    font-size: 22px;
    padding-left: 1rem;
  }
  transform: scale(0.5) translate(-50%, -50%);
}
.shopping-cart-inner-card {
  :deep(.p-card-content) {
    padding-top: 0;
    padding-bottom: 0;
  }
}
</style>
