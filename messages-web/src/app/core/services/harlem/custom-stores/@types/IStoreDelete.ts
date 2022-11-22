import type { Action } from '@harlem/extension-action';

export interface IStoreDelete {
  deleteItem: Action<string | number | symbol>;
}
