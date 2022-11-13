<template>
  <loading-status-handler>
    <data-view
      class="border-round no-background"
      :class="{ '-mx-1': viewLayout === 'grid' }"
      :layout="viewLayout"
      :value="items"
    >
      <template #list="{ data, index }">
        <div class="col-12">
          <data-card
            class="shadow-none border-noround"
            :class="{
              'border-round-top': index === 0,
              'border-round-bottom': index === (items?.length ?? 0) - 1,
            }"
            :data="data"
          >
          </data-card>
        </div>
      </template>
      <template #grid="{ data }">
        <div class="col-12 md:col-6 xl:col-4 p-1">
          <data-card class="h-full" :data="data"></data-card>
        </div>
      </template>
    </data-view>
  </loading-status-handler>
</template>

<script lang="ts">
import { screenLarge } from '@/app/core/services/window/window.service';
import { defineComponent, computed } from 'vue';
import { itemsCollectionProvider } from '../../providers/items-collection.provider';

export default defineComponent({
  setup() {
    const wrappedItems = itemsCollectionProvider.inject();
    if (wrappedItems.value == null) {
      throw new Error('невозможно показать коллекцию элементов');
    }

    const items = wrappedItems.value();

    const viewLayout = computed(() => (screenLarge.value ? 'grid' : 'list'));

    return {
      items,
      viewLayout,
    };
  },
});
</script>

<style lang="scss" scoped>
.no-background {
  :deep(.p-dataview-content) {
    background-color: var(--surface-ground);
  }
}
</style>
