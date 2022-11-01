<template>
  <transition enter-active-class="scalein animation-duration-400" appear>
    <menu-bar :class="{ 'shadow-5': y > 20 }" :model="items"> </menu-bar>
  </transition>
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
