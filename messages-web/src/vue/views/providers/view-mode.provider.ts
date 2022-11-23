import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export type CatalogViewMode = 'user' | 'admin';

export const viewModeProvider = new Provider<CatalogViewMode>(() => ref('user'));
