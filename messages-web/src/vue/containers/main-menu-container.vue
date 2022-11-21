<template>
  <transition enter-active-class="scalein animation-duration-200" appear>
    <toolbar
      class="border-noround pl-2 pr-1 pb-0 pt-2 toolbar-start-w-full no-padding"
      :class="{ 'shadow-6 backdrop': y > 20 }"
    >
      <template #start>
        <div class="w-full">
          <app-container>
            <div class="flex flex-row justify-content-between align-items-center gap-3">
              <div class="flex gap-3">
                <img src="@/assets/images/logo.svg" alt="" width="30" height="24" />
                <div class="text-sm lg:text-lg">Маркетплейс продукции гражданского назначения</div>
              </div>
              <div class="flex flex-row flex-shrink-0 align-items-center">
                <theme-switch-container></theme-switch-container>
                <user-avatar-container />
              </div>
            </div>
          </app-container>
        </div>
      </template>
    </toolbar>
  </transition>
</template>

<script lang="ts">
import { useWindowScroll } from '@vueuse/core';
import { defineComponent } from 'vue';

export default defineComponent({
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
      // {
      //   label: 'О нас',
      //   to: { name: 'home' },
      //   icon: 'pi pi-info',
      // },
      // {
      //   label: 'Эксперименты',
      //   to: { name: 'labs' },
      //   icon: 'pi pi-verified',
      // },
    ];
    const { y } = useWindowScroll();

    return { items, y };
  },
});
</script>

<style lang="scss" scoped>
.toolbar-start-w-full {
  :deep(.p-toolbar-group-left) {
    width: 100%;
  }
}
.no-padding {
  :deep(.grid) {
    padding: 0;
    :deep(div) {
      padding: 0;
    }
  }
}

.backdrop {
  backdrop-filter: blur(1rem);
}
</style>
