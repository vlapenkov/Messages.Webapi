<script lang="ts">
import { computed, defineComponent, h, PropType, ref } from 'vue';
import Card from 'primevue/card';
import { useElementHover } from '@vueuse/core';

export type CardShadows = 'default' | 'none' | '1' | '2' | '4' | '5' | '6' | '7' | '8';

export default defineComponent({
  components: { Card },
  props: {
    title: {
      type: String,
      default: null,
    },
    shadow: {
      type: String as PropType<CardShadows>,
      default: 'none',
    },
    shadowHover: {
      type: String as PropType<Omit<CardShadows, 'default'>>,
      default: '3',
    },
    noPadding: {
      type: Boolean,
      default: false,
    },
    imageHeader: {
      type: Boolean,
      default: false,
    },
  },
  setup(props, { slots }) {
    const cardRef = ref();
    const isCardHovered = useElementHover(cardRef);
    const cardClasses = computed<string[]>(() => {
      const classes: string[] = ['re-padding-card'];
      if (props.shadow !== 'default') {
        classes.push(`shadow-${props.shadow}`);
      }
      if (isCardHovered.value && props.shadowHover !== 'none') {
        classes.push(`shadow-${props.shadowHover}`, 'trans-shadow');
      }
      if (props.noPadding) {
        classes.push('no-padding');
      }
      if (props.imageHeader) {
        classes.push('image-header');
      }
      return classes;
    });
    return () =>
      h(
        Card,
        { class: cardClasses.value, ref: cardRef },
        {
          ...slots,
          title: props.title == null ? slots.title : () => props.title,
          content: slots.default ?? slots.content,
        },
      );
  },
});
</script>

<style lang="scss" scoped>
.re-padding-card {
  &.image-header {
    :deep(.p-card-header) {
      line-height: 0;
      text-align: center;
    }
  }
  &.no-padding {
    :deep(.p-card-body) {
      padding: 0;
    }
  }

  :deep(.p-card-body) {
    height: var(--p-card-body-height) !important;
  }

  :deep(.p-card-content) {
    padding-bottom: 0;
    padding-top: 0;
    height: 100% !important;
  }
}
</style>
