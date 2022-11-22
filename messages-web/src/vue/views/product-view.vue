<template>
  <app-page :title="item.name">
    <div class="grid">
      <div class="col-3">
        <product-image></product-image>
      </div>
      <div class="col-7 flex flex-column justify-content-between">
        <div class="p-component text-lg">{{ item.name }}</div>
        <div class="p-component text-sm text-primary">{{ item.country }}</div>
        <div class="p-component text-sm">{{ item.price }} {{ item.currency }}</div>
        <div class="p-component text-sm text-info">{{ item.statusText }}</div>
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import { IQueryOtions } from '@/app/core/services/harlem/custom-stores/composables/@types/IQueryOptions';
import { productFullStore } from '@/app/product-full/state/product-full.store';
import { defineComponent, onMounted, watchEffect } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();

    const item = productFullStore.itemSmart();
    onMounted(() => {
      productFullStore.getDataAsync({
        force: true,
        arguments: {
          id:
            route.params.id && route.params.id !== '' && (route.params.id as string).trim() !== ''
              ? route.params.id
              : null,
        },
      } as IQueryOtions);
    });
    watchEffect(() => {
      console.log({ item });
    });
    return { item };
  },
});
</script>

<style scoped></style>
