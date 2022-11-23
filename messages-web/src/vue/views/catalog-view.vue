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
        <collection-state :modes="[{ mode: 'tree-view' }]" :state="sectionsStore" reload-on-save>
          <template #tree-view>
            <tree-view-collection
              v-model:selectedKeys="selectedKeys"
              selectionMode="single"
              class="reshape-tree"
            >
            </tree-view-collection>
          </template>
        </collection-state>
      </div>
      <div ref="productsContainerRef" class="col-9">
        <products-viewer :categoryId="selectedKey" />
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
import { TreeSelectionKeys } from 'primevue/tree';
import { computed, defineComponent, ref, watch } from 'vue';
import { viewModeProvider } from './providers/view-mode.provider';

export default defineComponent({
  setup() {
    const selectedKeys = ref<TreeSelectionKeys>();

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

    const selectedKey = computed(() => {
      const keys = selectedKeys.value;
      if (keys == null) {
        return undefined;
      }
      const sk = Object.keys(keys).find((k) => keys[k] === true);
      return sk == null ? undefined : +sk;
    });
    const productsContainerRef = ref<HTMLElement>();
    const { width: productsContainerSize } = useElementSize(productsContainerRef);

    return {
      sectionsStore,
      selectedKeys,
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
