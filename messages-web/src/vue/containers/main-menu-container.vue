<template>
  <transition enter-active-class="scalein animation-duration-200" appear>
    <menu-bar :class="{ 'shadow-6 backdrop': y > 20 }" :model="items">
      <template #end>
        <user-avatar-container />
      </template>
    </menu-bar>
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
      {
        label: 'Товары',
        to: { name: 'products' },
        icon: 'pi pi-list',
      },
      {
        label: 'Заказы',
        to: { name: 'orders' },
        icon: 'pi pi-box',
      },
    ];
    const { y } = useWindowScroll();

    return { items, y };
  },
});
</script>

<style lang="scss" scoped>
.backdrop {
  backdrop-filter: blur(1rem);
}
</style>
