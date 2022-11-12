import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export const pageNumberProvider = new Provider<number | undefined>(
  () => ref(),
  '--provide--page-number',
);
