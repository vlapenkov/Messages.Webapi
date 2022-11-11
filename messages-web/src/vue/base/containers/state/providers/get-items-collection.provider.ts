import { ModelBase } from '@/app/core/models/base/model-base';
import { IQueryOtions } from '@/app/core/services/harlem/custom-stores/tools/@types/IQueryOptions';
import { Provider } from '@/app/core/tools/provider';
import type { Action } from '@harlem/extension-action';
import { shallowRef, ShallowRef } from 'vue';

type GetData = Action<IQueryOtions | undefined, ModelBase[] | null>;

export const getItemsCollectionProvider = new Provider<GetData, ShallowRef<GetData | undefined>>(
  () => shallowRef(),
);
