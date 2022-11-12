import { IStoreSave } from '@/app/core/services/harlem/custom-stores/@types/IStoreSave';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

type Save = IStoreSave['saveChanges'] | undefined;

export const saveChangesProvider = new ShallowProvider<Save>(
  () => shallowRef(),
  '--provide--save-changes',
);
