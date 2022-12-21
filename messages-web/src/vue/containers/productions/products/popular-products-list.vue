<template>
  <toast position="top-right" group="tr" />
  <div class="w-full grid mr-0">
    <template v-if="productions == null">
      <div v-for="i in 12" :key="i" class="col-3">
        <skeleton height="350px" />
      </div>
    </template>
    <div v-else v-for="item in productions" :key="item.id" class="col-12 md:col-6 lg:col-3">
      <production-list-item :production="item" @notify="notifyHandler" />
    </div>
  </div>
</template>

<script lang="ts">
import { OrderByProduct } from '@/app/productions/models/OrderByProduct';
import { productionsService } from '@/app/productions/services/productions.service';
import { productionsStore } from '@/app/productions/state/productions.store';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';
import { useToastNotificationHandler } from '@/composables/toast-notification-handler.composable';
import Toast from 'primevue/toast';
import { useToast } from 'primevue/usetoast';
import { defineComponent, onMounted } from 'vue';

export default defineComponent({
  components: { Toast },
  setup() {
    const toast = useToast();

    onMounted(() => {
      const pageNumberDefault = 1;
      const pageSizeDefault = 12;
      productionsStore.pageNumber.value = pageNumberDefault;
      productionsStore.pageSize.value = pageSizeDefault;
      productionsService.loadPage({
        name: null,
        catalogSectionId: undefined,
        pageNumber: pageNumberDefault,
        pageSize: pageSizeDefault,
        producerName: null,
        region: null,
        orderBy: OrderByProduct.RatingByDesc,
      });
      shoppingCartStore.getDataAsync();
    });

    const notifyHandler = useToastNotificationHandler(toast);
    const { currentPageItems: productions } = productionsStore;
    return { productions, notifyHandler };
  },
});
</script>

<style lang="scss"></style>
