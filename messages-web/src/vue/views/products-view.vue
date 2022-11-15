<template>
  <div>
    <collection-state :modes="[{ label: 'Сеткой', mode: 'data-view' }]" :state="productsStore">
      <template #data-view>
        <data-view-collection></data-view-collection>
      </template>
    </collection-state>
  </div>
</template>

<script lang="ts">
import { productsStore, sectionId } from '@/app/products/state/products.store';
import { defineComponent, watch } from 'vue';
import { useRoute } from 'vue-router';

export default defineComponent({
  setup() {
    const route = useRoute();
    watch(
      () => route.params.sectionId,
      (id) => {
        if (id == null) {
          return;
        }
        sectionId.value = +id;
      },
    );
    return { productsStore };
  },
});
</script>

<style scoped></style>
