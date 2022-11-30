<template>
  <toast position="top-right" group="tr" />
  <div class="grid">
    <div v-for="item in productShortsItems" :key="item.id" class="col-2">
      <product-card
        :product="item"
        @addToCart="addProductToShopingCart"
        @viewProduct="viewProduct"
      />
    </div>
  </div>
</template>

<script lang="ts">
import { IproductsPageRequest } from '@/app/product-shorts/@types/IproductsPageRequest';
import { ProductShortModel } from '@/app/product-shorts/models/product-short.model';
import { productShortsService } from '@/app/product-shorts/services/product-shorts.service';
import { productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { addToCart } from '@/app/shopping-cart/infrastructure/shopping-cart.http-service';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { computed, defineComponent, onMounted } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  components: { Toast },
  setup() {
    onMounted(() => {
      const request: IproductsPageRequest = {
        name: null,
        catalogSectionId: undefined,
        pageNumber: 1,
        pageSize: 8,
      };
      productShortsService.loadPage(request);
    });
    const toast = useToast();
    const router = useRouter();
    const productShortsItems = computed(() => productShortsStore.currentPageItems.value);
    const addProductToShopingCart = async (model: ProductShortModel) => {
      await addToCart({
        productId: model.id,
        quantity: 1,
      });
      toast.add({
        severity: 'success',
        group: 'tr',
        detail: `${model.name} был успешно добавлен в корзину`,
        life: 3000,
      });
    };
    const viewProduct = (item: ProductShortModel) => {
      router.push({ name: 'product', params: { id: item.id } });
    };
    return { productShortsItems, addProductToShopingCart, viewProduct };
  },
});
</script>

<style lang="scss"></style>
