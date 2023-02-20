<template>
  <scroll-panel class="pr-2" :style="panelStyle">
    <slot></slot>
  </scroll-panel>
</template>

<script lang="ts">
import { useWindowSize } from '@vueuse/core';
import { computed, CSSProperties, defineComponent } from 'vue';
import { headerHeightProvider } from './providers/headerHeightProvider';

export default defineComponent({
  setup() {
    const headerHeight = headerHeightProvider.inject();
    const { height: windowHeight } = useWindowSize();
    const height = computed(() => windowHeight.value - headerHeight.value - 40);
    const panelStyle = computed<CSSProperties>(() => ({
      height: `${height.value}px`,
    }));
    return { panelStyle };
  },
});
</script>

<style scoped></style>
