<script lang="ts">
import Button from 'primevue/button';
import { defineComponent, h } from 'vue';

export const defaultButtonStyle = 'p-button-sm';

export const primeButtonProps = {
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
};

export default defineComponent({
  props: primeButtonProps,
  setup(props, { slots }) {
    return () =>
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      h(Button as any, { ...props }, { ...slots });
  },
});
</script>
<style lang="scss">
.blurred {
  backdrop-filter: blur(1rem);
}
</style>
