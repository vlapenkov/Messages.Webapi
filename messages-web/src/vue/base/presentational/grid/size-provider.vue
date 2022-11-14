<template>
  <div ref="divRef"></div>
</template>

<script lang="ts">
import { useElementSize } from '@vueuse/core';
import { defineComponent, ref } from 'vue';
import { containerSizeProvider } from './providers/container-size.provider';

export default defineComponent({
  setup() {
    const divRef = ref<HTMLElement>();
    const { width } = useElementSize(divRef);
    containerSizeProvider.provide(width);
    return { divRef };
  },
});
</script>

<style scoped></style>
