<template>
  <prime-button
    @click="dark = !dark"
    :icon="dark ? 'pi pi-moon' : 'pi pi-sun'"
    class="p-button-rounded no-label"
  ></prime-button>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import Button from 'primevue/button';
import { useDark } from '@vueuse/core';
import { useHead } from '@vueuse/head';

export default defineComponent({
  components: { PrimeButton: Button },
  setup() {
    const dark = useDark();
    const themeString = computed(() => (dark.value ? 'dark' : 'light'));
    useHead({
      link: [
        {
          rel: 'stylesheet',
          href: () => `themes/lara-${themeString.value}-indigo/theme.css`,
        },
      ],
    });
    return { dark };
  },
});
</script>

<style scoped></style>
