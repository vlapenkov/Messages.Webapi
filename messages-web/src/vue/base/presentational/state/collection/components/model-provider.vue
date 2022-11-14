<template>
  <slot></slot>
</template>

<script lang="ts">
import { ModelBase } from '@/app/core/models/base/model-base';
import { defineComponent, PropType } from 'vue';
import { modelProvider } from '../providers/model-provider';

export default defineComponent({
  props: {
    data: {
      type: Object as PropType<ModelBase>,
    },
  },
  setup(props) {
    modelProvider.provideFrom(() => props.data);
    return {};
  },
});
</script>

<style scoped></style>
