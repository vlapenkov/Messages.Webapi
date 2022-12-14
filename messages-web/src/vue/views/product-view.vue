<template>
  <app-page :title="item.name">
    <toast position="top-right" group="tr" />
    <card>
      <template #content>
        <div class="grid">
          <div class="col-3">
            <file-store-image
              :id="item?.documents[0]?.fileId"
              :maxHeight="300"
              :fitWidth="true"
            ></file-store-image>
          </div>
          <div class="col-7 flex flex-column">
            <product-info :product="item"></product-info>
            <div class="mt-4">
              <prime-button
                @click="addToCart(item.id, item.name)"
                class="p-button-sm h-full py-3 px-7"
                label="Заказать"
              >
              </prime-button>
            </div>
            <div>
              <div class="flex flex-row mt-2">
                <div class="flex-1 flex">
                  <prime-button
                    label="Добавить в избранное"
                    icon="pi pi-heart"
                    class="p-button-text py-1 button-light"
                  />
                </div>
                <div class="flex-1 flex">
                  <prime-button
                    label="Добавить к сравнению"
                    icon="pi pi-chart-bar"
                    class="p-button-text py-1 button-light"
                  />
                </div>
                <div class="flex-1 flex">
                  <prime-button
                    label="Искать аналоги"
                    icon="pi pi-arrows-h"
                    class="p-button-text py-1 button-light"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </template>
    </card>
    <card class="mt-2">
      <template #content>
        <tab-view ref="tabview1">
          <tab-panel header="Описание">
            <p>{{ item.description }}</p>
          </tab-panel>
          <tab-panel header="Технические характеристики">
            <product-attributes :product="item"> </product-attributes>
          </tab-panel>
        </tab-view>
      </template>
    </card>
  </app-page>
</template>

<script lang="ts">
import { IQueryOtions } from '@/app/core/services/harlem/custom-stores/composables/@types/IQueryOptions';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { defineComponent, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  components: { Toast },
  setup() {
    const route = useRoute();
    const toast = useToast();
    const item = productFullStore.itemSmart();
    watch(
      () => route.params.id as string | undefined,
      (id) => {
        if (id == null || id === '' || id.trim() === '') {
          return;
        }
        productFullStore.getDataAsync({
          force: true,
          arguments: {
            id,
          },
        } as IQueryOtions);
      },
      {
        immediate: true,
      },
    );

    const addProductToShopingCart = async (id: number, name: string) => {
      await shoppingCartStore.addToCart({
        productId: id,
        quantity: 1,
      });

      toast.add({
        severity: 'success',
        group: 'tr',
        detail: `${name} был успешно добавлен в корзину`,
        life: 3000,
      });
    };

    return { item, addToCart: addProductToShopingCart };
  },
});
</script>

<style scoped>
.button-light :deep(.p-button-label) {
  font-weight: 400;
  width: 170px;
  text-align: left;
}
</style>
