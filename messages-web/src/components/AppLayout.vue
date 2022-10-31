<template>
  <div class="relative min-h-screen ml-3 mr-3 pt-3 pb-3">
    <div ref="headRef" class="fixed top-0 left-0 pl-2 pr-2 pt-2 min-w-full">
      <slot name="head"></slot>
    </div>
    <div :style="bodyStyle">
      <slot name="body"></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { useElementSize } from '@vueuse/core';
import { computed, CSSProperties, defineComponent, ref } from 'vue';

export default defineComponent({
  setup() {
    const headRef = ref<HTMLDivElement>();
    const { height } = useElementSize(headRef);
    const bodyStyle = computed<CSSProperties>(() => ({
      paddingTop: `${height.value}px`,
    }));
    return { headRef, bodyStyle };
  },
});
</script>

<style lang="scss" scoped></style>
