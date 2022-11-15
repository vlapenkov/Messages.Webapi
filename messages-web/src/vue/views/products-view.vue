<template>
  <div class="min-h-full flex flex-column flex-grow-1">
    <collection-state
      :modes="[{ label: 'Сеткой', mode: 'data-view' }]"
      class="flex-grow-1"
      :state="productShortsStore"
    >
      <template #toolbar-end>
        <create-item-button :disabled="sectionId == null"></create-item-button>
      </template>
      <template #data-view>
        <data-view-collection background></data-view-collection>
      </template>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { productShortsStore, sectionId } from '@/app/product-shorts/state/products.store';
import { defineComponent, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
    watch(
      () => route.params.sectionId,
      (id) => {
        if (id == null) {
          sectionId.value = undefined;
        }
        sectionId.value = +id;
      },
    );
    return { productShortsStore, sectionId };
  },
});
</script>

<style scoped></style>
