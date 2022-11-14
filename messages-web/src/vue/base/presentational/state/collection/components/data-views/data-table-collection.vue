<template>
  <data-table :value="items"></data-table>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useItems } from '../../composables/items.composable';

export default defineComponent({
  setup() {
    const items = useItems();
    return { items };
  },
});
</script>

<style scoped></style>
