<template>
  <app-layout>
    <template #head>
      <main-menu-container></main-menu-container>
    </template>
    <template #body>
      <router-view />
    </template>
  </app-layout>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppLayout from './components/AppLayout.vue';

export default defineComponent({ components: { AppLayout } });
</script>

<style lang="scss"></style>
