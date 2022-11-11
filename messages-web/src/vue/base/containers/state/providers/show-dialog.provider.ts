import { Ref, ref } from 'vue';
import { Provider } from '@/app/core/tools/provider';

export const showDialogProvider = new Provider<boolean, Ref<boolean>>(
  () => ref(false),
  '--provide--show-dialog',
);
