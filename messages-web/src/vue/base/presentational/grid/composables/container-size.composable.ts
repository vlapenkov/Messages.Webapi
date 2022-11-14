import { WindowBreakpoints } from '@/app/core/services/window/window.service';
import { computed } from 'vue';
import { containerSizeProvider } from '../providers/container-size.provider';

export function useContainerSize() {
  const width = containerSizeProvider.inject();
  const isSm = computed(() => width.value >= WindowBreakpoints.Small);
  const isMd = computed(() => width.value >= WindowBreakpoints.Middle);
  const isLg = computed(() => width.value >= WindowBreakpoints.Large);
  const isXl = computed(() => width.value >= WindowBreakpoints.ExtraLArge);
  return { isSm, isMd, isLg, isXl };
}
