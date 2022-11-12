<script lang="ts">
import { ButtonEmits, ButtonProps } from 'primevue/button';
import { FunctionalComponent, h } from 'vue';
import PrimeButton from './base/prime-button.vue';

const PrimeButtonAdd: FunctionalComponent<ButtonProps, ButtonEmits> = (props, { slots }) =>
  h(
    PrimeButton,
    {
      ...props,
      icon: props.icon ?? 'pi pi-plus',
    },
    { ...slots },
  );

export default PrimeButtonAdd;
</script>
