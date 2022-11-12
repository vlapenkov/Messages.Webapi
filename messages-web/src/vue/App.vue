<template>
  <app-layout>
    <template #head>
      <main-menu-container></main-menu-container>
    </template>
    <template #body>
      <router-view v-slot="{ Component }">
        <transition-fade>
          <component :is="Component"></component>
        </transition-fade>
      </router-view>
    </template>
  </app-layout>
  <theme-switch-container class="fixed bottom-0 left-0 mb-2 ml-2 z-2"></theme-switch-container>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppLayout from './components/app-layout.vue';
import MainMenuContainer from './containers/main-menu-container.vue';
import ThemeSwitchContainer from './base/containers/theme-switch-container.vue';

export default defineComponent({
  components: { AppLayout, MainMenuContainer, ThemeSwitchContainer },
});
</script>

<style lang="scss"></style>
