import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export const containerSizeProvider = new Provider<number>(() => ref(0));
