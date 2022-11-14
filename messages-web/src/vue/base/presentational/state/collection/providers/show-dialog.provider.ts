import { ref } from 'vue';
import { Provider } from '@/app/core/tools/provider';

export const showDialogProvider = new Provider<boolean>(() => ref(false), '--provide--show-dialog');
