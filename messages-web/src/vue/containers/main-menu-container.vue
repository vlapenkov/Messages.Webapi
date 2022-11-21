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
              <div class="flex gap-3 align-items-center">
                <img src="@/assets/images/logo.svg" alt="" width="30" height="24" />
                <div class="text-sm lg:text-lg">Маркетплейс продукции гражданского назначения</div>
              </div>
              <div class="flex flex-row align-items-center flex-shrink-0">
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
    const { y } = useWindowScroll();

    return { y };
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
