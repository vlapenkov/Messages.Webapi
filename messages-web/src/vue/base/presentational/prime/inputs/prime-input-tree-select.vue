<!-- eslint-disable vuejs-accessibility/label-has-for -->
<template>
  <div class="field">
    <span class="p-float-label">
      <tree-select :id="id" v-model="value" v-bind="{ disabled }" :options="options"></tree-select>
      <label :for="id">{{ label }}</label>
    </span>
  </div>
</template>

<script lang="ts">
import { TreeNode } from 'primevue/tree';
import { computed, defineComponent, PropType } from 'vue';
import { inputProps } from './input-props';

export default defineComponent({
  props: {
    modelValue: {
      type: Object as PropType<TreeNode>,
    },
    options: {
      type: Array as PropType<TreeNode[]>,
    },
    id: {
      type: String,
      default: 'inputTreeSelect',
    },
    label: {
      type: String,
      default: 'inputTreeSelect',
    },
    ...inputProps,
  },
  emits: {
    'update:modelValue': (_: TreeNode | undefined) => true,
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
