<template>
  <app-page title="Корзина">
    <div class="flex flex-column">
      <div v-for="item in items" :key="item.productId">
        <div class="grid">
          <div class="col-2">
            <product-image :id="item.documentId"></product-image>
          </div>
          <div class="col-4 flex flex-column gap-3">
            <div class="p-component text-md">{{ item.productName }}</div>
            <div class="p-component text-md text-primary">{{ item.organization.name }}</div>
          </div>
          <div class="col-4 flex flex-column gap-3">
            <div class="p-component text-md">Цена: {{ item.price }}</div>
          </div>
          <div class="col-2">
            <div class="p-component text-md">{{ item.quantity }} шт.</div>
          </div>
        </div>
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import { createOrder } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import { ShoppingCartModel } from '@/app/shopping-cart/models/shopping-cart.model';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { defineComponent, ref, WritableComputedRef } from 'vue';
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
    const items = state.items() as WritableComputedRef<ShoppingCartModel[]>;
    const router = useRouter();
    const create = async () => {
      await createOrder();
      router.push({ name: 'orders' });
    };
    return {
      shoppingCartStore,
      create,
      items,
    };
  },
});
</script>

<style scoped></style>
