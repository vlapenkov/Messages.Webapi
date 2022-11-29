<template>
  <app-page :title="item.name">
    <card>
      <template #content>
        <div class="grid">
          <div class="col-3">
            <product-image :documents="item.documents"></product-image>
          </div>
          <div class="col-7 flex flex-column">
            <product-info :product="item"></product-info>
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
import { defineComponent, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
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

    return { item };
  },
});
</script>

<style scoped></style>
