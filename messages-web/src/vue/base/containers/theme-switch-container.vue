<template>
  <prime-button
    @click="dark = !dark"
    :icon="!dark ? 'pi pi-moon' : 'pi pi-sun'"
    class="p-button-rounded p-button-text p-button-outlined p-button-sm no-label icon-small p-button-secondary"
  ></prime-button>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import { useHead } from '@vueuse/head';
import { isDark } from '@/store/theme.store';

export default defineComponent({
  setup() {
    const dark = isDark;
    const themeString = computed(() => (dark.value ? 'dark' : 'light'));
    useHead({
      link: [
        {
          rel: 'stylesheet',
          href: () => `/themes/lara-${themeString.value}-blue/theme.css`,
        },
        {
          rel: 'stylesheet',
          href: '/themes/primevue.min.css',
        },
      ],
    });
    return { dark };
  },
});
</script>

<style lang="scss" scoped>
$size: 0;
$padding-and-font: 1rem;
.p-button.icon-small {
  height: $size !important;
  width: $size;
  padding: $padding-and-font;
  :deep(.p-button-icon) {
    font-size: $padding-and-font;
  }
}
</style>
