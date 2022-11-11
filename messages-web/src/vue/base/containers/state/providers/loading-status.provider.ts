import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef, WritableComputedRef } from 'vue';

type StatusRef = WritableComputedRef<DataStatus> | undefined;

export const loadingStatusProvider = new Provider<StatusRef, ShallowRef<StatusRef>>(
  () => shallowRef(),
  '--provide--lodading-status',
);
