<script lang="ts">
import Button, { ButtonEmits, ButtonProps } from 'primevue/button';
import { FunctionalComponent, h } from 'vue';

export const defaultButtonStyle = 'p-button-rounded';

const PrimeButton: FunctionalComponent<ButtonProps, ButtonEmits> = (props, { slots }) =>
  h(Button, { ...props, class: props.class ?? defaultButtonStyle }, { ...slots });

export default PrimeButton;
</script>
