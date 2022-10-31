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
import MainMenuContainer from './containers/MainMenuContainer.vue';

export default defineComponent({ components: { AppLayout, MainMenuContainer } });
</script>

<style lang="scss"></style>
