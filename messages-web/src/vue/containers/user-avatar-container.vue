<!-- eslint-disable vuejs-accessibility/click-events-have-key-events -->
<template>
  <div
    aria-controls="overlay_menu"
    aria-haspopup="true"
    @click="toggleMenu"
    class="flex flex-row align-items-center gap-2 p-1 pl-3 avatar border-round-3xl"
    v-if="isAuthenticated"
  >
    <div>
      {{ userShortName }}
    </div>
    <avatar shape="circle" :image="gravatarUrl"></avatar>
  </div>
  <prime-menu class="mt-1" id="overlay_menu" ref="menu" :model="menuItems" :popup="true" />
</template>

<script lang="ts">
import { isAuthenticated, userInfo } from '@/store/user.store';
import { computed, defineComponent, ref } from 'vue';
import { url } from 'gravatar';
import { screenMiddle } from '@/app/core/services/window/window.service';
import { logout } from '@/app/core/services/keycloak/keycloak.service';
import Menu from 'primevue/menu';

const avatarSize = 100;
export default defineComponent({
  components: { PrimeMenu: Menu },
  setup() {
    const gravatarUrl = computed(() =>
      userInfo.value == null ? undefined : url(userInfo.value.email, { s: `${avatarSize}` }),
    );
    const userShortName = computed(() =>
      [userInfo.value?.familyName ?? '', userInfo.value?.givenName ?? '']
        .map((part) => (!screenMiddle.value ? `${part[0]}.` : part))
        .join(' '),
    );

    const menuItems = [
      {
        label: 'Каталог товаров',
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
        label: 'Отчеты',
        to: { name: 'reports' },
        icon: 'pi pi-th-large',
      },
      {
        label: 'Выход',
        icon: 'pi pi-sign-out',
        command: () => {
          logout();
        },
      },
    ];

    const menu = ref();

    const toggleMenu = (event: Event) => {
      console.log('menu', menu.value);

      menu.value.toggle(event);
    };

    return { isAuthenticated, gravatarUrl, userShortName, menu, menuItems, toggleMenu };
  },
});
</script>

<style lang="scss" scoped>
.avatar {
  transition: background-color 0.35s ease-in-out;
  transition: color 0.35s ease-in-out;
  &:hover {
    background-color: var(--surface-ground);
    cursor: pointer;
  }
}
</style>
