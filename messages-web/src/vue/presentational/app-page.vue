<template>
  <h1 class="p-component text-xl sm:text-2xl">{{ title }}</h1>
  <slot></slot>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  props: {
    title: {
      type: String,
      default: 'Без названия',
    },
  },
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
