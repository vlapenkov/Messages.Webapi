import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export const headerHeightProvider = new Provider<number>(() => ref(0), '--header-height');
