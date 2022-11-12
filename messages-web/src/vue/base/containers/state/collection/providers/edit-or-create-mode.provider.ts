import { DataMode } from '@/app/core/services/harlem/tools/not-valid-data';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

export const editOrCreateModeProvider = new ShallowProvider<DataMode | undefined>(() =>
  shallowRef(),
);
