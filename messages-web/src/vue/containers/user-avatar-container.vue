<!-- eslint-disable vuejs-accessibility/click-events-have-key-events -->
<template>
  <div aria-controls="overlay_menu" aria-haspopup="true" @click="toggleMenu"
    class="flex flex-row align-items-center gap-2 p-1 pl-3 avatar border-round-3xl" v-if="isAuthenticated">
    <div>{{ userShortName }}</div>
    <avatar shape="circle" icon="pi pi-user"></avatar>
  </div>
  <prime-menu class="mt-1" id="overlay_menu" ref="menu" :model="menuItems" :popup="true">
  </prime-menu>
</template>

<script lang="ts">
import { isAuthenticated, userInfo } from '@/store/user.store';
import { computed, defineComponent, ref } from 'vue';
// import { url } from 'gravatar';
import { screenMiddle } from '@/app/core/services/window/window.service';
import { logout } from '@/app/core/services/keycloak/keycloak.service';
import Menu from 'primevue/menu';
import { shoppingCartStore } from '@/app/shopping-cart/state/shopping-cart.store';

// const avatarSize = 100;
export default defineComponent({
  components: { PrimeMenu: Menu },
  setup() {
    const gravatarUrl = '@/assets/imagesg/avatar_placeholder.png';

    // computed(() =>
    //   userInfo.value == null ? undefined : url(userInfo.value.email, { s: `${avatarSize}` }),
    // );

    const orgs: Record<string, string> = {
      '5907001774': 'НПО «ИСКРА»',
      '6312139922': 'Прогресс',
      '7404052938': 'Златоустовский машиностроительный завод',
      '5047008220': 'НПО Энергомаш',
      '7804588900': 'АО «КБ «Арсенал»',
      '7743254939': 'АО НТЦ «Охрана»',
      '5042006211': 'ФКП «НИЦ РКП»',
      '9710021379': 'ООО «СБ «РК-Страхование»',
    };

    const userShortName = computed(() => {
      let user = [userInfo.value?.familyName ?? '', userInfo.value?.givenName ?? '']
        .map((part) => (!screenMiddle.value ? `${part[0]}.` : part))
        .join(' ');
      if (userInfo.value?.inn != null && orgs[userInfo.value.inn] != null) {
        user = `${user}, ${orgs[userInfo.value.inn]}`;
      }
      return user;
    });

    const menuItems = computed(() => {
      const items = [];
      const roles = userInfo.value?.role
      if (roles != null && roles.indexOf("manager_org_seller") >= 0) {
        items.push(
          {
            label: 'Товары',
            to: { name: 'products' },
            icon: 'pi pi-bars',
          },
          {
            label: 'Заказы',
            to: { name: 'orders' },
            icon: 'pi pi-box',
          },
          {
            label: 'Отчеты',
            to: { name: 'reports' },
            icon: 'pi pi-chart-bar',
          })
      }
      if (roles != null && roles.indexOf("manager_org_buyer") >= 0) {
        items.push({
          label: 'Каталог товаров',
          to: { name: 'catalog' },
          icon: 'pi pi-th-large',
        },
          {
            label: `Корзина${shoppingCartStore.totalQuantity.value > 0
              ? ` (${shoppingCartStore.totalQuantity.value})`
              : ''
              }`,
            to: { name: 'shopping-cart' },
            icon: 'pi pi-shopping-cart',
            badge: 5,
          },
          {
            label: 'Заказы',
            to: { name: 'orders' },
            icon: 'pi pi-box',
          },
          {
            label: 'География производства',
            to: { name: 'production-geo' },
            icon: 'pi pi-map',
          })
      }
      if (roles != null && roles.indexOf("content_manager") >= 0) {
        items.push(
          {
            label: 'Управление товарами',
            to: { name: 'products' },
            icon: 'pi pi-bars',
          },
          {
            label: 'Управление категориями',
            to: { name: 'categories' },
            icon: 'pi pi-book',
          },

          {
            label: 'Общие отчеты',
            to: { name: 'reports' },
            icon: 'pi pi-chart-bar',
          },
          {
            label: 'Управление организациями',
            to: { name: 'organization-add' },
            icon: 'pi pi-plus',
          })
      }
      items.push({
        label: 'Выход',
        icon: 'pi pi-sign-out',
        command: () => {
          logout();
        },
      })
      return items;
    });

    const menu = ref();

    const toggleMenu = (event: Event) => {
      // console.log('menu', menu.value);
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

.decoration-none {
  text-decoration: none;
}
</style>
