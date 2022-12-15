import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export const searchHeightProvider = new Provider(() => ref(0));
