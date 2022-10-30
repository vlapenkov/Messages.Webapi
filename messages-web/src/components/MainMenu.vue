<template>
  <menu-bar :class="{ 'shadow-6': y > 20 }" :model="items"></menu-bar>
</template>

<script lang="ts">
import { useWindowScroll } from '@vueuse/core';
import Menubar from 'primevue/menubar';
import { defineComponent } from 'vue';

export default defineComponent({
  components: { MenuBar: Menubar },
  setup() {
    const items = [
      {
        label: 'Главная',
        to: { name: 'home' },
      },
      {
        label: 'О нас',
        to: { name: 'about' },
      },
      {
        label: 'Категории',
        to: { name: 'sections' },
      },
    ];
    const { y } = useWindowScroll();

    return { items, y };
  },
});
</script>

<style scoped></style>
