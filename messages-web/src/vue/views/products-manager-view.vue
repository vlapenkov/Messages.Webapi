<template>
  <div class="flex flex-row justify-content-end">
    <router-link class="no-underline" :to="{ name: 'create-product' }">
      <prime-button-add label="Добавить товар"></prime-button-add>
    </router-link>
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
