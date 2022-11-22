<template>
  <app-page title="Каталог товаров">
    <template #subheader>
      <div class="flex-grow-1">
        <span class="p-input-icon-left w-full">
          <i class="pi pi-search" />
          <input-text class="w-full" type="text" placeholder="Search" />
        </span>
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
      <div class="col-9">
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
import { TreeSelectionKeys } from 'primevue/tree';
import { computed, defineComponent, ref, watch } from 'vue';

export default defineComponent({
  setup() {
    const selectedKeys = ref<TreeSelectionKeys>();

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
    return {
      sectionsStore,
      selectedKeys,
      selectedKey,
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
