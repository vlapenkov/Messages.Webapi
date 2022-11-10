import { ICollectionStoreSave } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreSave';
import { Provider } from '@/app/core/tools/provider';
import { shallowRef, ShallowRef } from 'vue';

type Save = ICollectionStoreSave['saveChanges'] | undefined;

export const saveChangesProvider = new Provider<Save, ShallowRef<Save>>(() => shallowRef());
