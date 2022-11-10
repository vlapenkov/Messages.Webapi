import { Ref, ref } from 'vue';
import { Provider } from '@/app/core/tools/provider';

export const reloadOnSaveProvider = new Provider<boolean, Ref<boolean>>(
  () => ref(false),
  '--provide--reload-on-save',
);
