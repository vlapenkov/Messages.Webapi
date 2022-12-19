<template>
  <div>
    <div class="flex flex-row justify-content-between gap-5 align-items-center">
      <app-text class="p-component my-3" mode="header">{{ title }}</app-text>
      <slot name="subheader"></slot>
    </div>
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
