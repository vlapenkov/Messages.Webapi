<template>
  <menu-bar :class="{ 'shadow-5': y > 20 }" :model="items">
    <template #end>
      <theme-switcher></theme-switcher>
    </template>
  </menu-bar>
</template>

<script lang="ts">
import { useWindowScroll } from '@vueuse/core';
import Menubar from 'primevue/menubar';
import { defineComponent } from 'vue';
import ThemeSwitcher from './ThemeSwitcher.vue';

export default defineComponent({
  components: { MenuBar: Menubar, ThemeSwitcher },
  setup() {
    const items = [
      {
        label: 'Главная',
        to: { name: 'home' },
        icon: 'pi pi-home',
      },
      {
        label: 'О нас',
        to: { name: 'about' },
        icon: 'pi pi-info',
      },
      {
        label: 'Категории',
        to: { name: 'sections' },
        icon: 'pi pi-folder',
      },
    ];
    const { y } = useWindowScroll();

    return { items, y };
  },
});
</script>

<style scoped></style>
