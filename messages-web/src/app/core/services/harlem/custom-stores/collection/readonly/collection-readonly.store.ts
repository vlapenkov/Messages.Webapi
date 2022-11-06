import { Constructor } from '@/app/@types/constructor';
import { extend } from '@/app/core/handlers/base/handler-lab';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionHttpService } from '@/app/core/services/http/custom/collection.http-service';
import { parseArray } from '@/app/core/services/http/handlers/parse.handlers';
import type { Action } from '@harlem/extension-action';
import { onMounted, WritableComputedRef } from 'vue';
import { createDefaultStore, DefaultStore } from '../../../harlem.service';
import { DataStatus } from '../../../tools/data-status';

import { CollectionReadonlyState } from './collection-readonly.state';

export interface IReadonlyCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> {
  readonly status: WritableComputedRef<DataStatus>;
  readonly itemsAsync: (ops?: { force: boolean }) => WritableComputedRef<TModel[] | null>;
  readonly getDataAsyncAction: Action<{ force: boolean } | undefined, TModel[] | null>;
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
): [DefaultStore<TState>, IReadonlyCollectionStore<TIModel, TModel>] {
  const stateDefault = new State();
  const store: DefaultStore<TState> = createDefaultStore(name, stateDefault);
  const { computeState, action } = store;

  const status = computeState((state) => state.status);

  const items = computeState((state) => state.items);

  const getDataAsyncActionKey = 'get-data-async';

  const getDataAsyncAction: Action<{ force: boolean } | undefined, TModel[] | null> = action(
    getDataAsyncActionKey,
    async (ops: { force: boolean } = { force: false }) => {
      const currentStatus = status.value.status;
      if (!ops.force && currentStatus === 'loaded') {
        return items.value;
      }

      status.value = new DataStatus(currentStatus === 'initial' ? 'loading' : 'updating');
      const requestFn = extend(service.get).pipe(parseArray(Model)).done();
      const response = await requestFn();
      if (response.status === HttpStatus.Success) {
        items.value = response.data ?? null;
        status.value = new DataStatus('loaded');
      } else {
        status.value = new DataStatus('error', response.message);
      }

      return items.value;
    },
  );

  const itemsAsync = (ops: { force: boolean } = { force: false }) => {
    onMounted(() => {
      if (ops.force || status.value.status === 'initial') {
        getDataAsyncAction(ops);
      }
    });
    return items;
  };

  const extended: IReadonlyCollectionStore<TIModel, TModel> = {
    status,
    itemsAsync,
    getDataAsyncAction,
    items,
  } as const;

  return [store, extended];
}
