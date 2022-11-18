<template>
  <transition enter-active-class="scalein animation-duration-200" appear>
    <menu-bar :class="{ 'shadow-6 backdrop': y > 20 }" :model="items">
      <template #end>
        <div class="flex flex-row align-items-center gap-3">
          <theme-switch-container></theme-switch-container>
          <user-avatar-container />
        </div>
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
        label: 'Товары',
        to: { name: 'sections' },
        icon: 'pi pi-th-large',
      },
      {
        label: 'Корзина',
        to: { name: 'shopping-cart' },
        icon: 'pi pi-shopping-cart',
      },
      {
        label: 'Заказы',
        to: { name: 'orders' },
        icon: 'pi pi-box',
      },
      {
        label: 'О нас',
        to: { name: 'home' },
        icon: 'pi pi-info',
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
