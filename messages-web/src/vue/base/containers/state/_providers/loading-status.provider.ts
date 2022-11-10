import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { Provider } from '@/app/core/tools/provider';
import { Ref, ref } from 'vue';

export const loadingStatusProvider = new Provider<DataStatus, Ref<DataStatus | undefined>>(
  () => ref<DataStatus>(),
  '--provide--lodading-status',
);
