<template>
  <divider class="top-solid">
    <slot></slot>
  </divider>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  setup() {
    return {};
  },
});
</script>

<style lang="scss" scoped>
.top-solid {
  &::before {
    border-top-style: solid !important;
  }
}
</style>
