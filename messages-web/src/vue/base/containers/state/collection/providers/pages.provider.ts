import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export const pageNumberProvider = new Provider<number | undefined>(
  () => ref(),
  '--provide--page-number',
);

export const pageSizeProvider = new Provider<number | undefined>(
  () => ref(),
  '--provide--page-size',
);

export const totalItemsCountProvider = new Provider<number | undefined>(
  () => ref(),
  '--provide--total-items-count',
);
