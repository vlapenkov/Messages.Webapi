<template>
  <app-layout>
    <template #head>
      <main-menu-container></main-menu-container>
    </template>
    <template #body>
      <router-view v-slot="{ Component }">
        <transition-fade>
          <auth-guard>
            <component :is="Component"></component>
          </auth-guard>
        </transition-fade>
      </router-view>
    </template>
    <template #footer>
      <footer-container />
    </template>
  </app-layout>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import AppLayout from './presentational/app-layout.vue';
import MainMenuContainer from './containers/main-menu-container.vue';

export default defineComponent({
  components: { AppLayout, MainMenuContainer },
});
</script>

<style lang="scss"></style>
