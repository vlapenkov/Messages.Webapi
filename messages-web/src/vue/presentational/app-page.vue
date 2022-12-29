<template>
  <div :class="{ 'mt-4': hasPrefix }">
    <slot name="prefix"></slot>
    <div class="flex flex-row justify-content-between gap-5 align-items-center">
      <app-text v-if="!hideTitle" class="p-component my-3" mode="header">{{ title }}</app-text>
      <slot name="subheader"></slot>
    </div>
    <slot></slot>
  </div>
</template>

<script lang="ts">
import { useTitle } from '@vueuse/core';
import { computed, defineComponent, watch } from 'vue';

export default defineComponent({
  props: {
    title: {
      type: String,
      default: 'Без названия',
    },
    hideTitle: {
      type: Boolean,
      default: false,
    },
  },
  setup(props, { slots }) {
    const title = useTitle();
    const hasPrefix = computed(() => slots.prefix != null);
    watch(
      () => props.title,
      (titleValue) => {
        title.value = `${titleValue} | Маркетплейс продукции гражданского назначения`;
      },
      {
        immediate: true,
      },
    );
    return { hasPrefix };
  },
});
</script>

<style scoped></style>
