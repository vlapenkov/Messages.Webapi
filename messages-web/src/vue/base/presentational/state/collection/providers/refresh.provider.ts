import type { Action } from '@harlem/extension-action';
import { ShallowProvider } from '@/app/core/tools/shallow.provider';
import { shallowRef } from 'vue';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
type GetData = Action<any, any>;

export const refreshProvider = new ShallowProvider<GetData | undefined>(
  () => shallowRef(),
  '--provide--get-items-collection',
);
