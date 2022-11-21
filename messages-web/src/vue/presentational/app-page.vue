<template>
  <div>
    <h1 class="p-component text-xl sm:text-2xl">{{ title }}</h1>
    <slot></slot>
  </div>
</template>

<script lang="ts">
import { useTitle } from '@vueuse/core';
import { defineComponent, watch } from 'vue';

export default defineComponent({
  props: {
    title: {
      type: String,
      default: 'Без названия',
    },
  },
  setup(props) {
    const title = useTitle();
    watch(
      () => props.title,
      (titleValue) => {
        title.value = `${titleValue} | Маркетплейс продукции гражданского назначения`;
      },
      {
        immediate: true,
      },
    );
    return {};
  },
});
</script>

<style scoped></style>
