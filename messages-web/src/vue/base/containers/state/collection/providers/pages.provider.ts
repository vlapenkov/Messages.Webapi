import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export const pageNumberProvider = new Provider<number>(() => ref(0), '--provide--page-number');

export const pageSizeProvider = new Provider<number>(() => ref(0), '--provide--page-size');

export const totalItemsCountProvider = new Provider<number>(
  () => ref(0),
  '--provide--total-items-count',
);
