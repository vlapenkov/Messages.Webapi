<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="field">
    <span class="p-float-label">
      <dropdown v-model="value" v-bind="{ options, optionLabel, optionValue, id, disabled }" />
      <label :for="id">{{ label }}</label>
    </span>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import { inputProps } from './input-props';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export type Option = any;

export default defineComponent({
  props: {
    modelValue: {},
    id: {
      type: String,
      default: 'inputDropdown',
    },
    label: {
      type: String,
      default: 'inputDropdown',
    },
    options: {
      type: Array,
    },
    optionLabel: {
      type: [String, Function],
    },
    optionValue: {
      type: [String, Function],
    },
    ...inputProps,
  },
  emits: {
    'update:modelValue': (_: Option) => true,
  },
  setup(props, { emit }) {
    const value = computed({
      get: () => props.modelValue,
      set: (val) => {
        emit('update:modelValue', val);
      },
    });
    return { value };
  },
});
</script>

<style scoped></style>
