<template>
  <div class="grid">
    <div class="col-12 lg:col-10 xl:col-8 lg:col-offset-1 xl:col-offset-2">
      <slot></slot>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {};
  },
});
</script>

<style scoped></style>
