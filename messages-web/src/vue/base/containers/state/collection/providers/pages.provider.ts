import { Provider } from '@/app/core/tools/provider';
import { Ref, ref } from 'vue';

export const pageNumberProvider = new Provider<Ref<number> | undefined>(
  () => ref(),
  '--provide--page-number',
);

export const pageSizeProvider = new Provider<Ref<number> | undefined>(
  () => ref(),
  '--provide--page-size',
);

export const totalItemsCountProvider = new Provider<number>(
  () => ref(0),
  '--provide--total-items-count',
);
