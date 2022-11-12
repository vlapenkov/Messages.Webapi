import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef, WritableComputedRef } from 'vue';

type StatusRef = WritableComputedRef<DataStatus> | undefined;

export const loadingStatusProvider = new ShallowProvider<StatusRef>(
  () => shallowRef(),
  '--provide--lodading-status',
);
