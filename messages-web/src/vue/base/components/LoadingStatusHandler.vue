<template>
  <transition mode="out-in" enter-active-class="fadein" leave-active-class="fadeout">
    <div v-if="status?.status === 'loaded'">
      <slot></slot>
    </div>
    <progress-spinner v-else></progress-spinner>
  </transition>
</template>

<script lang="ts">
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { defineComponent, PropType } from 'vue';

export default defineComponent({
  props: {
    status: Object as PropType<DataStatus>,
  },
});
</script>

<style scoped></style>
