<template>
  <app-layout>
    <template #head>
      <main-menu></main-menu>
    </template>
    <template #body>
      <router-view />
    </template>
  </app-layout>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppLayout from './components/AppLayout.vue';
import MainMenu from './components/MainMenu.vue';

export default defineComponent({ components: { AppLayout, MainMenu } });
</script>

<style lang="scss"></style>
