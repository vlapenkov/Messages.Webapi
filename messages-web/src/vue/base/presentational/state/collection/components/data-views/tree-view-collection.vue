<template>
  <loading-status-handler>
    <tree class="mt-2" :value="tree"></tree>
  </loading-status-handler>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { useTree } from '../../composables/tree.composable';

export default defineComponent({
  setup() {
    return { tree: useTree() };
  },
});
</script>

<style scoped></style>
