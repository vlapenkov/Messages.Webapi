import type { Action } from '@harlem/extension-action';

export interface ICollectionStoreDelete {
  deleteItem: Action<string | number | symbol>;
}
