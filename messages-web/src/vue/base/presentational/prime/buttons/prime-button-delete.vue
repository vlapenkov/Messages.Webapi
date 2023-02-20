<script lang="ts">
import { ButtonEmits, ButtonProps } from 'primevue/button';
import { FunctionalComponent, h } from 'vue';
import PrimeButton, { defaultButtonStyle } from './base/prime-button.vue';

const PrimeButtonSave: FunctionalComponent<ButtonProps, ButtonEmits> = (props, { slots }) =>
  h(
    PrimeButton,
    {
      ...props,
      icon: props.icon ?? 'pi pi-trash',
      class: `p-button-danger ${defaultButtonStyle}`,
    },
    { ...slots },
  );

export default PrimeButtonSave;
</script>
