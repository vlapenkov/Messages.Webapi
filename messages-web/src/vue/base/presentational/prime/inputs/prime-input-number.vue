<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="field">
    <span class="p-float-label">
      <input-number :id="id" v-model="value" />
      <label :for="id">{{ label }}</label>
    </span>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import { inputProps } from './input-props';

export default defineComponent({
  props: {
    modelValue: {
      type: Number,
    },
    id: {
      type: String,
      default: 'inputNumber',
    },
    label: {
      type: String,
      default: 'inputNumber',
    },
    ...inputProps,
  },
  emits: {
    'update:modelValue': (_: number | undefined) => true,
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
