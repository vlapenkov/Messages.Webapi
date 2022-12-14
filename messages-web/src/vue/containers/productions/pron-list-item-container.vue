<template>
  <card v-if="production == null">
    <template #title> I Am null (Work in progress) </template>
  </card>
  <product-card
    v-else-if="production.productionType === 'Product'"
    :product="production"
    @addToCart="addProductToShopingCart"
    @viewProduct="viewProduct"
    @viewOrganization="viewOrganization"
  />
  <card v-else-if="production.productionType === 'ServiceProduct'">
    <template #title> Я услуга </template>
  </card>
  <card v-else-if="production.productionType === 'WorkProduct'">
    <template #title> Я работа </template>
  </card>
</template>

<script lang="ts">
import { ProductionModel } from '@/app/productions/models/production.model';
import { productionsStore } from '@/app/productions/state/productions.store';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { ToastMessageOptions } from 'primevue/toast';
import { computed, defineComponent, PropType } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  props: {
    production: {
      type: Object as PropType<ProductionModel>,
    },
  },
  emits: {
    notify(_: ToastMessageOptions) {
      return true;
    },
  },
  setup(props, { emit }) {
    const router = useRouter();
    const productShortsItems = computed(() => productionsStore.currentPageItems.value);
    const addProductToShopingCart = async (model: ProductionModel) => {
      await shoppingCartStore.addToCart({
        productId: model.id,
        quantity: 1,
      });
      emit('notify', {
        severity: 'success',
        group: 'tr',
        detail: `${model.name} был успешно добавлен в корзину`,
        life: 3000,
      });
    };
    const viewProduct = () => {
      if (props.production == null) {
        return;
      }
      router.push({ name: 'product', params: { id: props.production.id } });
    };
    const viewOrganization = () => {
      if (props.production == null) {
        return;
      }
      router.push({ name: 'organization', params: { id: props.production.organization.id } });
    };
    return { productShortsItems, addProductToShopingCart, viewProduct, viewOrganization };
  },
});
</script>

<style scoped></style>
