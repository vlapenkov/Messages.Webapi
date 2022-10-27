import { Constructor } from '@/app/@types/constructor';
import { extend } from '@/app/core/handlers/base/handler-lab';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { computedAsync } from '@vueuse/core';
import { ICollectionHttpService } from '../../../http/custom/collection.http-service';
import { parseArray } from '../../../http/handlers/parse.handlers';
import { DataStatus } from '../../tools/data-status';
import { createDefaultStore } from '../../harlem.service';
import { CollectionState } from './collection.state';

export function createCollectionStore<TIModel extends IModel, TModel extends ModelBase<TIModel>>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<CollectionState<TModel>>,
  service: ICollectionHttpService<TIModel>,
) {
  const stateDefault = new State();
  const store = createDefaultStore(name, stateDefault);
  const { computeState, action } = store;

  const loadingStatus = computeState((state) => state.status);

  const items = computeState((state) => state.items);

  const itemSelected = computeState((state) => state.itemSelected);

  const getDataAsyncAction = action(
    'get-data-async',
    async (ops: { force: boolean } = { force: false }) => {
      const currentStatus = loadingStatus.value.status;
      if (!ops.force && currentStatus === 'loaded') {
        return null;
      }
      loadingStatus.value = new DataStatus(currentStatus === 'initial' ? 'loading' : 'updating');
      const response = await extend(service.get).pipe(parseArray(Model)).done()();
      if (response.status === HttpStatus.Success) {
        items.value = response.data ?? null;
        loadingStatus.value = new DataStatus('loaded');
      } else {
        loadingStatus.value = new DataStatus('error', response.message);
      }
      return items.value;
    },
  );

  const itemsAsync = (ops: { force: boolean } = { force: false }) =>
    computedAsync(() => getDataAsyncAction(ops), null);

  const extended = { loadingStatus, itemsAsync, items, itemSelected } as const;

  return [store, extended] as const;
}
