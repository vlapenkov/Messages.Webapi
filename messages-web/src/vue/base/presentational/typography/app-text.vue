<script lang="ts">
import { computed, defineComponent, h, PropType } from 'vue';

export default defineComponent({
  props: {
    tag: {
      type: String,
      default: 'span',
    },
    mode: {
      type: String as PropType<
        'primary' | 'default' | 'weak' | 'weaker' | 'header' | 'header-strong' | 'subheader'
      >,
      default: 'default',
    },
  },
  setup(props, { slots }) {
    const textStyle = computed<string[]>(() => ['text', `text-${props.mode}`]);
    return () =>
      h(
        // eslint-disable-next-line @typescript-eslint/no-explicit-any
        props.tag as any,
        { class: textStyle.value },
        slots.default == null ? () => null : slots.default(),
      );
  },
});
</script>

<style lang="scss" scoped>
@import '@/assets/styles/_variables.scss';
.text {
  font-style: normal;

  &#{&}-primary {
    color: map-get($map: $colors-text, $key: 'primary');
    font-weight: 400;
    font-size: 14px;
    line-height: 17px;
  }
  &#{&}-default {
    color: map-get($map: $colors-text, $key: 'default');
    font-weight: 400;
    font-size: 16px;
    line-height: 19px;
  }
  &#{&}-image {
    color: map-get($map: $colors-text, $key: 'default');
    font-style: normal;
    font-weight: 500;
    font-size: 18px;
    line-height: 22px;
  }
  &#{&}-weak {
    color: map-get($map: $colors-text, $key: 'weak');
    font-weight: 400;
    font-size: 14px;
    line-height: 17px;
  }
  &#{&}-weaker {
    color: map-get($map: $colors-text, $key: 'weak');
    font-weight: 400;
    font-size: 12px;
    line-height: 17px;
  }
  &#{&}-header {
    color: map-get($map: $colors-text, $key: 'header');
    font-weight: 500;
    font-size: 26px;
    line-height: 31px;
  }
  &#{&}-subheader {
    color: map-get($map: $colors-text, $key: 'strong');
    font-weight: 500;
    font-size: 22px;
    line-height: 26px;
  }
  &#{&}-header-strong {
    color: map-get($map: $colors-text, $key: 'strong');
    font-weight: 500;
    font-size: 28px;
    line-height: 34px;
  }
}
</style>
