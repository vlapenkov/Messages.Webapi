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
  <theme-switch-container class="fixed bottom-0 right-0 mb-3 mr-3"></theme-switch-container>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppLayout from './components/AppLayout.vue';
import TransitionFade from './components/transitions/TransitionFade.vue';
import MainMenuContainer from './containers/MainMenuContainer.vue';
import ThemeSwitchContainer from './containers/ThemeSwitchContainer.vue';

export default defineComponent({
  components: { AppLayout, MainMenuContainer, ThemeSwitchContainer, TransitionFade },
});
</script>

<style lang="scss"></style>
