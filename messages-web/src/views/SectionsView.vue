<template>
  <h3>{{ loadingStatus.status }}</h3>
  <div v-if="loadingStatus.message">{{ loadingStatus.message }}</div>
  <template v-if="items !== null">
    <div v-for="(i, n) in items" :key="n">
      {{ json(i) }}
    </div>
  </template>
</template>

<script lang="ts">
import { sectionsStore } from '@/app/sections/state/sections.store';
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const { itemsAsync, loadingStatus } = sectionsStore;
    const items = itemsAsync();
    return {
      items,
      loadingStatus,
      json: (i: unknown) => JSON.stringify(i, null, 2),
    };
  },
});
</script>

<style scoped></style>
