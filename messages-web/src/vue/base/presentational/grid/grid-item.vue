<template>
  <div :class="itemClasses"></div>
</template>

<script lang="ts">
import { computed, defineComponent, PropType } from 'vue';
import { ISizeBazedStyle } from './@types/ISizeBasedStyle';
import { useContainerSize } from './composables/container-size.composable';

export default defineComponent({
  props: {
    itemStyle: {
      type: Object as PropType<Partial<ISizeBazedStyle>>,
    },
  },
  setup(props) {
    const { isSm, isMd, isLg, isXl } = useContainerSize();
    const itemClasses = computed(() => {
      if (isXl.value && props.itemStyle?.xl != null) {
        return props.itemStyle.xl;
      }
      if (isLg.value && props.itemStyle?.lg != null) {
        return props.itemStyle.lg;
      }
      if (isMd.value && props.itemStyle?.md != null) {
        return props.itemStyle.md;
      }
      if (isSm.value && props.itemStyle?.sm != null) {
        return props.itemStyle.sm;
      }
      if (props.itemStyle?.default != null) {
        return props.itemStyle.default;
      }
      return '';
    });
    return { itemClasses };
  },
});
</script>

<style scoped></style>
