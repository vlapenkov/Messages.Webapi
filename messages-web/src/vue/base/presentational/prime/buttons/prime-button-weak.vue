<script lang="ts">
import { defineComponent, h } from 'vue';
import primeButtonVue, { primeButtonProps } from './base/prime-button.vue';

export default defineComponent({
  props: {
    ...primeButtonProps,
    small: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
  },
  emits: ['click'],
  setup(props, { emit, slots }) {
    return () =>
      h(
        primeButtonVue,
        {
          ...props,
          onClick: () => {
            emit('click');
          },
          class: {
            'btn-weak': true,
            'small': props.small,
            'disabled': props.disabled,
            'hover': !props.disabled,
          },
        },
        slots,
      );
  },
});
</script>

<style lang="scss" scoped>
@import '@/assets/styles/_variables.scss';
$button-background: map-get(
  $map: $colors,
  $key: 'text-buttton-background',
);
.btn-weak {
  background: $button-background;
  border: none;
  color: map-get($map: $colors-text, $key: 'button');
  font-style: normal;
  font-weight: 400;
  font-size: 14px;
  &:not(.small) {
    padding: 0.99rem 1.25rem;
  }

  &.small {
    height: 36px;
  }
  &.disabled {
    background: map-get($map: $colors, $key: 'text-buttton-disabled');
  }
  &.hover {
    &:hover {
      background: darken($button-background, 3);
      color: map-get($map: $colors-text, $key: 'button');
    }
  }
  box-shadow: none;
}
</style>
