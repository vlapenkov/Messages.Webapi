<template>
  <prime-button
    @click="dark = !dark"
    :icon="!dark ? 'pi pi-moon' : 'pi pi-sun'"
    class="p-button-rounded no-label p-button-secondary"
    :class="{ blurred: dark }"
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
          href: () => `themes/lara-${themeString.value}-indigo/theme.css`,
        },
        {
          rel: 'stylesheet',
          href: 'themes/primevue.min.css',
        },
      ],
    });
    return { dark };
  },
});
</script>

<style lang="scss" scoped>
.blurred {
  backdrop-filter: blur(0.5rem);
}
</style>
