import type { ModelBase } from '@/app/core/models/base/model-base';
import type { IQueryOtions } from '@/app/core/services/harlem/custom-stores/tools/@types/IQueryOptions';
import type { Action } from '@harlem/extension-action';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

type GetData = Action<IQueryOtions | undefined, ModelBase[] | null>;

export const getItemsCollectionProvider = new ShallowProvider<GetData | undefined>(
  () => shallowRef(),
  '--provide--get-items-collection',
);
