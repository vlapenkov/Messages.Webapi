<script lang="ts">
import { ButtonEmits, ButtonProps } from 'primevue/button';
import { FunctionalComponent, h } from 'vue';
import PrimeButton from './base/PrimeButton.vue';

const PrimeButtonSave: FunctionalComponent<ButtonProps, ButtonEmits> = (props, { slots }) =>
  h(
    PrimeButton,
    {
      ...props,
      icon: props.icon ?? 'pi pi-save',
    },
    { ...slots },
  );

export default PrimeButtonSave;
</script>
