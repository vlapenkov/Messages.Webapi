<template>
  <div v-if="useFixedWidth" class="flex flex-row justify-content-center">
    <div class="flex-grow-1 flex-shrink-1" style="max-width: 1200px">
      <slot></slot>
    </div>
  </div>
  <div v-else class="grid">
    <div class="col-12 lg:col-10 xl:col-8 lg:col-offset-1 xl:col-offset-2">
      <slot></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { screenWidth } from '@/app/core/services/window/window.service';
import { computed, defineComponent } from 'vue';

export default defineComponent({
  setup() {
    const useFixedWidth = computed(() => screenWidth.value >= 1200);
    return { useFixedWidth };
  },
});
</script>

<style scoped></style>
