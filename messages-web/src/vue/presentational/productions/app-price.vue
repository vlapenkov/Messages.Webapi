<template>
  <div v-if="price != null && price > 0" class="text-lg font-bold">{{ price }} ₽</div>
  <tag v-else class="align-self-start tag-green" value="Primary" rounded>
    <span class="text-greeny text-sm font-normal">Цена по запросу</span>
  </tag>
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

<style lang="scss" scoped>
.tag-green {
  background-color: #d5fef3;
}
.text-greeny {
  color: #00ba88;
}
</style>
