<template>
  <transition mode="out-in" enter-active-class="fadein" leave-active-class="fadeout">
    <div v-if="status?.value.status === 'loaded'">
      <slot></slot>
    </div>
    <div v-else class="flex justify-content-center align-items-center h-20rem">
      <slot name="loading">
        <progress-spinner></progress-spinner>
      </slot>
    </div>
  </transition>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { loadingStatusProvider } from '../containers/state/collection/providers/loading-status.provider';

export default defineComponent({
  setup() {
    const status = loadingStatusProvider.inject();
    return { status };
  },
});
</script>

<style scoped></style>
