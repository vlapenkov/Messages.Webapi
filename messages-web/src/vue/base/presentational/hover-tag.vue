<script lang="ts">
import { useElementHover } from '@vueuse/core';
import { defineComponent, h, ref } from 'vue';

export default defineComponent({
  props: {
    tag: {
      type: String,
      default: 'div',
    },
  },

  setup(props, { slots }) {
    const elRef = ref<HTMLElement>();
    const isHovered = useElementHover(elRef);
    const defaultSlot = slots.default;
    return () =>
      h(
        props.tag,
        {
          ref: elRef,
        },
        defaultSlot == null ? 'no-content' : defaultSlot({ hover: isHovered.value }),
      );
  },
});
</script>

<style scoped></style>
