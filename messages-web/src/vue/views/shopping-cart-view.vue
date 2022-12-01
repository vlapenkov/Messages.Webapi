<template>
  <app-page title="Корзина">
    <div class="flex flex-column">
      <card class="shopping-cart-inner-card">
        <template #content>
          <shopping-cart-item v-for="item in items" :key="item.productId" :item="item" />
          <div class="flex flex-row justify-content-between align-items-center mt-1">
            <div class="p-component text-lg font-semibold">Общая стоимость: {{ sum }} ₽</div>
            <prime-button
              @click="createNewOrder"
              label="Перейти к оформлению"
              :disabled="createNewOrderDisabled"
            ></prime-button>
          </div>
        </template>
      </card>
    </div>
  </app-page>
</template>

<script lang="ts">
import { computed, defineComponent, ref, WritableComputedRef } from 'vue';
import { createOrder } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { ShoppingCartModel } from '@/app/shopping-cart/models/shopping-cart.model';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
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
    const createNewOrder = async () => {
      await createOrder();
    };
    const createNewOrderDisabled = computed(() => (items.value ?? []).length === 0);
    const sum = computed(() =>
      (items.value ?? []).map((i) => i.price * i.quantity).reduce((acc, curr) => acc + curr, 0),
    );
    return {
      sum,
      items,
      createNewOrderDisabled,
      createNewOrder,
    };
  },
});
</script>

<style lang="scss" scoped>
.shopping-cart-inner-card {
  :deep(.p-card-content) {
    padding-top: 0;
    padding-bottom: 0;
  }
}
</style>
