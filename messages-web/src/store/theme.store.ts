import { createDefaultStore } from '@/app/core/services/harlem/harlem.service';
import { useDark } from '@vueuse/core';
import { computed } from 'vue';

const isDarkUsed = useDark();

const themeStoreDefault = {
  dark: isDarkUsed.value,
};

const { getter, mutation } = createDefaultStore('theme', themeStoreDefault);

const getDark = getter('get-theme', (state) => state.dark);

const setDark = mutation<boolean>('set-dark', (state, dark) => {
  state.dark = dark;
  isDarkUsed.value = dark;
});

export const isDark = computed<boolean>({
  get: () => getDark.value,
  set: (val) => setDark(val),
});
