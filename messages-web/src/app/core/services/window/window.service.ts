import { useWindowSize } from '@vueuse/core';
import { computed } from 'vue';

const { width: screenWidth } = useWindowSize();

export enum WindowBreakpoints {
  Small = 576,
  Middle = 768,
  Large = 992,
  ExtraLArge = 1200,
}
/** >= 576px  */
export const screenSmall = computed(() => screenWidth.value >= WindowBreakpoints.Small);
/** >= 768px  */
export const screenMiddle = computed(() => screenWidth.value >= WindowBreakpoints.Middle);
/** >= 992px  */
export const screenLarge = computed(() => screenWidth.value >= WindowBreakpoints.Large);
/** >= 1200px  */
export const screenExtraLarge = computed(() => screenWidth.value >= WindowBreakpoints.ExtraLArge);
