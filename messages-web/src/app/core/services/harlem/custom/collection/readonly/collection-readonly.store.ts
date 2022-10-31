import { Constructor } from '@/app/@types/constructor';
import { extend } from '@/app/core/handlers/base/handler-lab';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionHttpService } from '@/app/core/services/http/custom/collection.http-service';
import { parseArray } from '@/app/core/services/http/handlers/parse.handlers';
import { computedAsync } from '@vueuse/core';
import { WritableComputedRef, Ref } from 'vue';
import { createDefaultStore } from '../../../harlem.service';
import { DataStatus } from '../../../tools/data-status';

import { CollectionReadonlyState } from './collection-readonly.state';

export interface IReadonlyCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> {
  readonly loadingStatus: WritableComputedRef<DataStatus>;
  readonly itemsAsync: (ops?: { force: boolean }) => Ref<TModel[] | null>;
  readonly items: WritableComputedRef<TModel[] | null>;
}

export function createCollectionReadonlyStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends CollectionReadonlyState<TModel>,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<TState>,
  service: ICollectionHttpService<TIModel>,
) {
  const stateDefault = new State();
  const store = createDefaultStore(name, stateDefault);
  const { computeState, action } = store;

  const loadingStatus = computeState((state) => state.status);

  const items = computeState((state) => state.items);

  const getDataAsyncAction = action(
    'get-data-async',
    async (ops: { force: boolean } = { force: false }) => {
      const currentStatus = loadingStatus.value.status;
      if (!ops.force && currentStatus === 'loaded') {
        return null;
      }

      loadingStatus.value = new DataStatus(currentStatus === 'initial' ? 'loading' : 'updating');
      const requestFn = extend(service.get).pipe(parseArray(Model)).done();
      const response = await requestFn();
      if (response.status === HttpStatus.Success) {
        items.value = response.data ?? null;
        loadingStatus.value = new DataStatus('loaded');
      } else {
        loadingStatus.value = new DataStatus('error', response.message);
      }
      // console.log('items:', items.value);

      return items.value;
    },
  );

  const itemsAsync = (ops: { force: boolean } = { force: false }) =>
    computedAsync(
      async () => {
        await getDataAsyncAction(ops);
        return items.value;
      },
      null,
      { lazy: true },
    );

  const extended: IReadonlyCollectionStore<TIModel, TModel> = {
    loadingStatus,
    itemsAsync,
    items,
  } as const;

  return [store, extended] as const;
}
