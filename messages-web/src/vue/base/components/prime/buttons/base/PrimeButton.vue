<script lang="ts">
import { isDark } from '@/store/theme.store';
import Button from 'primevue/button';
import { computed, defineComponent, h } from 'vue';

export const defaultButtonStyle = 'p-button-rounded';

export default defineComponent({
  props: {
    label: {
      type: String,
    },
    icon: {
      type: String,
    },
    iconPos: {
      type: String,
      default: 'left',
    },
    iconClass: {
      type: String,
      default: null,
    },
    badge: {
      type: String,
    },
    badgeClass: {
      type: String,
      default: null,
    },
    loading: {
      type: Boolean,
      default: false,
    },
    loadingIcon: {
      type: String,
      default: 'pi pi-spinner pi-spin',
    },
  },
  setup(props, { slots, attrs }) {
    const buttonStyle = computed(() => ({
      [defaultButtonStyle]: true,
      ' p-button-outlined': isDark.value,
    }));
    return () =>
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      h(Button as any, { ...props, class: attrs.class ?? buttonStyle.value }, { ...slots });
  },
});
</script>
