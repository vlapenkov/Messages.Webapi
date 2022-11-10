import { IStoreSave } from '@/app/core/services/harlem/custom-stores/collection/@types/IStoreSave';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef } from 'vue';

type Save = IStoreSave['saveChanges'] | undefined;

export const saveChangesProvider = new Provider<Save, ShallowRef<Save>>(() => shallowRef());
