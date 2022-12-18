<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="field">
    <span class="p-float-label" :class="spanStyle">
      <i v-if="icon" :class="icon" />
      <input-text :id="id" :class="inputStyle" type="text" v-model="value" />
      <label :for="id">{{ label }}</label>
    </span>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, PropType } from 'vue';
import { useSize } from './composables/size.composable';
import { inputProps } from './input-props';

export type IconPlacement = 'left' | 'right';

export default defineComponent({
  props: {
    modelValue: {
      type: String,
    },
    id: {
      type: String,
      default: 'inputText',
    },
    label: {
      type: String,
      default: 'inputText',
    },
    iconPlacement: {
      type: String as PropType<IconPlacement>,
      default: 'left',
    },
    icon: {
      type: String,
    },
    ...inputProps,
  },
  emits: {
    'update:modelValue': (_: string | undefined) => true,
  },
  setup(props, { emit }) {
    const value = computed({
      get: () => props.modelValue,
      set: (val) => {
        emit('update:modelValue', val);
      },
    });
    const spanStyle = computed<string[]>(() => {
      const classes: string[] = [];
      if (props.icon != null) {
        switch (props.iconPlacement) {
          case 'left':
            classes.push(' p-input-icon-left');
            break;
          case 'right':
            classes.push(' p-input-icon-right');
            break;
          default:
            throw new Error('Неизвестное положение иконки');
        }
      }
      return classes;
    });

    const inputStyle = useSize(props.size);

    return { value, spanStyle, inputStyle };
  },
});
</script>

<style scoped></style>
