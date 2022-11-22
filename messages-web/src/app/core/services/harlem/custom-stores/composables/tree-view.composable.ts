import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { WritableComputedRef } from 'vue';
import { DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getCollectionProp } from '../../state/decorators/property-keys/collection.prop-key';
import { getTreeViewTransform } from '../../state/decorators/property-keys/tree-view.prop-key';
import { IQueryOtions } from './@types/IQueryOptions';

export function useTreeView<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends StateBase,
>(
  store: DefaultStore<TState>,
  getItems: (arg?: IQueryOtions) => WritableComputedRef<TModel[] | null>,
) {
  const collectionKey = getCollectionProp(store.state) as string;
  if (collectionKey == null) {
    throw new Error('Cannot create tree-view without transformer');
  }
  const treeviewTransform = getTreeViewTransform(store.state, collectionKey);

  return treeviewTransform == null
    ? undefined
    : (arg?: IQueryOtions) => {
        const items = getItems(arg);
        return store.getter('get-tree-view', () => treeviewTransform(items.value ?? []));
      };
}
