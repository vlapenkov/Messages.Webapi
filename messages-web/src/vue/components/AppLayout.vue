<template>
  <div
    class="relative min-h-screen ml-3 mr-3 pt-3 pb-3 flex flex-column justify-content-between gap-3"
  >
    <div ref="headRef" class="fixed top-0 left-0 pl-2 pr-2 pt-2 min-w-full z-2">
      <slot name="head"></slot>
    </div>
    <div :style="bodyStyle" class="flex-grow-1 mb-7">
      <slot name="body"></slot>
    </div>
    <div v-if="hasFooter">
      <prime-divider></prime-divider>
      <slot name="footer"></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { useElementSize } from '@vueuse/core';
import { computed, CSSProperties, defineComponent, ref } from 'vue';

export default defineComponent({
  setup(_, { slots }) {
    const headRef = ref<HTMLDivElement>();
    const { height } = useElementSize(headRef);
    const bodyStyle = computed<CSSProperties>(() => ({
      paddingTop: `${height.value}px`,
    }));
    const hasFooter = computed(() => slots.footer != null);
    return { headRef, bodyStyle, hasFooter };
  },
});
</script>

<style lang="scss" scoped></style>
