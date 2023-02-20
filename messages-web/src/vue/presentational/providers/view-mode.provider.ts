import { Provider } from '@/app/core/tools/provider';
import { ref } from 'vue';

export type ViewMode = 'list' | 'grid';

export const viewModeProvider = new Provider<ViewMode>(() => ref('grid'));
