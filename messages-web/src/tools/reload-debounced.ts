import { useSessionStorage } from '@vueuse/core';
import { watch } from 'vue';

export function initReloader({
  maxReloads,
  debounceInSeconds,
}: {
  maxReloads: number;
  debounceInSeconds: number;
}) {
  const counter = useSessionStorage('__window_reload_counter', 0);
  let timeout: ReturnType<typeof setTimeout> | null = null;
  watch(counter, (cnt) => {
    if (cnt > 1) {
      timeout = setTimeout(() => {
        counter.value = 0;
      }, debounceInSeconds * 1000);
    }
  });

  return () => {
    if (counter.value > maxReloads) {
      return;
    }
    if (timeout) {
      clearTimeout(timeout);
    }
    counter.value = counter.value ? counter.value + 1 : 1;
    window.location.reload();
  };
}
