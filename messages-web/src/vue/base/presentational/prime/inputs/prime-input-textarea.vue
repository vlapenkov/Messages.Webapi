<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="field">
    <span class="p-float-label">
      <prime-textarea
        :id="id"
        auto-resize
        :class="inputStyle"
        type="text"
        v-bind="{ disabled }"
        v-model="value"
      />
      <label :for="id">{{ label }}</label>
    </span>
  </div>
</template>

<script lang="ts">
import { PrimeTextarea } from '@/tools/prime-vue-components';
import { computed, defineComponent } from 'vue';
import { useSize } from './composables/size.composable';
import { inputProps } from './input-props';

export type IconPlacement = 'left' | 'right';

export default defineComponent({
  components: { PrimeTextarea },
  props: {
    modelValue: {
      type: String,
    },
    id: {
      type: String,
      default: 'inputTextarea',
    },
    label: {
      type: String,
      default: 'inputTextarea',
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

    const inputStyle = useSize(props.size);
    return { value, inputStyle };
  },
});
</script>

<style scoped></style>
