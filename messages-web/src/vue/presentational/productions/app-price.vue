<template>
  <div v-if="price != null && price > 0" class="text-lg font-bold">{{ price }} ₽</div>
  <tag v-else class="align-self-start" value="Primary" rounded>Цена по запросу</tag>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  props: {
    price: {
      type: Number,
    },
  },
});
</script>

<style scoped></style>
