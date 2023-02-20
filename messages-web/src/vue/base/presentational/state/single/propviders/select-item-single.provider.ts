import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

export const selectItemSingleProvider = new ShallowProvider<
  ((payload?: void | undefined) => void) | null
>(() => shallowRef(null), '--state--select-single-item');
