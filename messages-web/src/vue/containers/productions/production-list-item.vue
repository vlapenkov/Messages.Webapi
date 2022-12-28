<template>
  <card v-if="production == null">
    <template #title> I Am null (Work in progress) </template>
  </card>
  <product-card
    v-else-if="viewMode === 'grid'"
    :id="'product' + production.id"
    :product="production"
    @addToCart="addProductToShopingCart"
    @viewProduct="viewProduct"
    @viewOrganization="viewOrganization"
  />
  <product-card-flat v-else></product-card-flat>
</template>

<script lang="ts">
import { ProductionModel } from '@/app/productions/models/production.model';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { breadcrumbStore } from '@/store/breadcrumb.store';
import { registerStore } from '@/store/register.store';
import { isAuthenticated } from '@/store/user.store';
import { viewModeProvider } from '@/vue/presentational/providers/view-mode.provider';
import { ToastMessageOptions } from 'primevue/toast';
import { defineComponent, PropType } from 'vue';
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
    const { showDialog } = registerStore;
    const { tree: breadcrumbs, breadcrumb } = breadcrumbStore;
    console.log(breadcrumb('/catalog?sectionId=4'));

    const addProductToShopingCart = async (model: ProductionModel) => {
      if (!isAuthenticated.value) {
        if (props.production == null) {
          return;
        }
        let routeLocation;
        switch (props.production.productionType) {
          case 'Product':
            routeLocation = router.resolve({
              name: 'product',
              params: { id: props.production.id },
            });
            break;
          case 'ServiceProduct':
            routeLocation = router.resolve({
              name: 'product-service',
              params: { id: props.production.id },
            });
            break;
          case 'WorkProduct':
            routeLocation = router.resolve({
              name: 'product-work',
              params: { id: props.production.id },
            });
            break;
          default:
            break;
        }
        showDialog(routeLocation);
        return;
      }
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
      switch (props.production.productionType) {
        case 'Product':
          router.push({ name: 'product', params: { id: props.production.id } });
          break;
        case 'ServiceProduct':
          router.push({ name: 'product-service', params: { id: props.production.id } });
          break;
        case 'WorkProduct':
          router.push({ name: 'product-work', params: { id: props.production.id } });
          break;
        default:
          break;
      }
    };
    const viewOrganization = () => {
      if (props.production == null) {
        return;
      }
      router.push({ name: 'organization', params: { id: props.production.organization.id } });
    };
    const viewMode = viewModeProvider.inject();
    return { addProductToShopingCart, viewProduct, viewOrganization, viewMode, breadcrumbs };
  },
});
</script>

<style scoped></style>
