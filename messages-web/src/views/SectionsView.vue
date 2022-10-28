<template>
  <div v-if="items == null">Loading...</div>
  <template v-else>
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
    const { itemsAsync } = sectionsStore;
    return { items: itemsAsync(), json: (i: unknown) => JSON.stringify(i, null, 2) };
  },
});
</script>

<style scoped></style>
