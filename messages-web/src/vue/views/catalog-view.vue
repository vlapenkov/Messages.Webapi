<template>
  <app-page title="Каталог товаров">
    <template #subheader>
      <div
        :style="{ maxWidth: productsContainerSize + 'px' }"
        class="flex-grow-1 flex flex-row justify-content-between gap-2"
      >
        <span class="p-input-icon-left w-full">
          <i class="pi pi-search" />
          <input-text class="w-full" v-model="search" type="text" placeholder="Поиск" />
        </span>
        <prime-button
          class="flex-shrink-0"
          @click="switchViewMode"
          :label="viewMode === 'user' ? 'Администрирование' : 'Представление пользователя'"
        ></prime-button>
      </div>
    </template>
    <div class="grid">
      <div class="col-3">
        <sections-container v-model:selected="selectedKey"></sections-container>
      </div>
      <div ref="productsContainerRef" class="col-9">
        <products-viewer :categoryId="selectedKey"></products-viewer>
      </div>
    </div>
  </app-page>
</template>

<script lang="ts">
import { IproductsPageRequest } from '@/app/product-shorts/@types/IproductsPageRequest';
import { productShortsService } from '@/app/product-shorts/services/product-shorts.service';
import { productShortsStore } from '@/app/product-shorts/state/product-shorts.store';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { useElementSize } from '@vueuse/core';
import { defineComponent, ref, watch } from 'vue';
import { viewModeProvider } from './providers/view-mode.provider';

export default defineComponent({
  setup() {
    const viewMode = viewModeProvider.provide();

    const switchViewMode = () => {
      viewMode.value = viewMode.value === 'user' ? 'admin' : 'user';
    };

    watch(
      [
        productShortsStore.pageNumber,
        productShortsStore.pageSize,
        productShortsStore.searchQuery,
        productShortsStore.parentSectionId,
      ],
      ([pageNumber, pageSize, query, catalogSectionId]) => {
        console.log('Запрашиваем страницы', pageNumber, pageSize, query, catalogSectionId);

        const request: IproductsPageRequest = {
          name: query == null || query === '' || query.trim() === '' ? null : query,
          catalogSectionId,
          pageNumber,
          pageSize,
        };
        productShortsService.loadPage(request);
      },
      {
        immediate: true,
      },
    );

    const selectedKey = ref<number>();
    const productsContainerRef = ref<HTMLElement>();
    const { width: productsContainerSize } = useElementSize(productsContainerRef);

    return {
      sectionsStore,
      selectedKey,
      search: productShortsStore.searchQuery,
      productsContainerRef,
      productsContainerSize,
      viewMode,
      switchViewMode,
    };
  },
});
</script>

<style lang="scss" scoped>
.reshape-tree {
  :deep(.p-tree) {
    margin-top: 0 !important;
    border: none;
  }
}
</style>
