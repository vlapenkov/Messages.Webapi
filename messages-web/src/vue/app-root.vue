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
import { defineComponent, onMounted } from 'vue';
import { authService } from '@/app/core/services/keycloak/auth.service';
import AppLayout from './presentational/app-layout.vue';
import MainMenuContainer from './containers/main-menu-container.vue';

export default defineComponent({
  components: { AppLayout, MainMenuContainer },
  setup() {
    onMounted(async () => {
      await authService.init();
    });
    return {};
  },
});
</script>

<style lang="scss"></style>
