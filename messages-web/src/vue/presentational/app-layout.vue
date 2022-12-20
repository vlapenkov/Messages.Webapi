<template>
  <div class="relative min-h-screen ml-2 mr-2 flex flex-column justify-content-between gap-3">
    <div ref="headRef" class="fixed top-0 left-0 min-w-full z-2">
      <slot name="head"></slot>
    </div>
    <div :style="bodyStyle" class="flex-grow-1">
      <app-container>
        <slot name="body"></slot>
      </app-container>
    </div>
    <div v-if="hasFooter" class="-mx-2">
      <slot name="footer"></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { useElementSize } from '@vueuse/core';
import { computed, CSSProperties, defineComponent, ref } from 'vue';
import { headerHeightProvider } from './providers/headerHeightProvider';

export default defineComponent({
  setup(_, { slots }) {
    const headRef = ref<HTMLDivElement>();
    const { height } = useElementSize(headRef);
    const bodyStyle = computed<CSSProperties>(() => ({
      paddingTop: `${height.value}px`,
    }));
    headerHeightProvider.provide(height);
    const hasFooter = computed(() => slots.footer != null);
    const headBackStyle = computed<CSSProperties>(() => ({
      height: `${height.value}px`,
    }));
    return { headRef, bodyStyle, hasFooter, headBackStyle };
  },
});
</script>

<style lang="scss" scoped>
.blurred {
  backdrop-filter: blur(0.5rem);
}
</style>
