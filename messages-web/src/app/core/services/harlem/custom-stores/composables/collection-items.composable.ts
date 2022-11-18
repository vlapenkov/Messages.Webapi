import { Constructor } from '@/app/@types/constructor';
import { extend } from '@/app/core/handlers/base/handler-lab';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { WritableComputedRef, onMounted } from 'vue';
import { ICollectionHttpService } from '../../../http/custom/collection.http-service';
import { parseArray } from '../../../http/handlers/parse.handlers';
import { DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getCollectionProp } from '../../state/decorators/property-keys/collection.prop-key';
import { getIgnoreMountedOpt } from '../../state/decorators/property-keys/ignore-mounted.prop-key';
import { DataStatus } from '../../tools/data-status';
import { IQueryOtions } from './@types/IQueryOptions';

export function useCollectionItems<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends StateBase,
>(
  store: DefaultStore<TState>,
  Model: Constructor<TModel>,
  service: ICollectionHttpService<TIModel>,
  status: WritableComputedRef<DataStatus>,
) {
  const collectionKey = getCollectionProp(store.state);
  if (collectionKey == null) {
    throw new Error('Cannot create collection store from non-collection state');
  }

  const itemsDumb = store.computeState(
    (state) => state[collectionKey as keyof TState] as unknown as TModel[] | null,
  );

  const ignoreMounted = getIgnoreMountedOpt(store.state, collectionKey as string) ?? false;

  const getDataAsync: Action<{ force: boolean } | undefined, TModel[] | null> = store.action(
    'get-data-async',
    async (ops: { force: boolean } = { force: false }) => {
      const currentStatus = status.value.status;
      if (!ops.force && currentStatus === 'loaded') {
        return itemsDumb.value;
      }

      status.value = new DataStatus(currentStatus === 'initial' ? 'loading' : 'updating');
      const requestFn = extend(service.get).pipe(parseArray(Model)).done();
      const response = await requestFn();
      if (response.status === HttpStatus.Success) {
        itemsDumb.value = response.data ?? null;
        status.value = new DataStatus('loaded');
      } else {
        status.value = new DataStatus('error', response.message);
      }

      return itemsDumb.value;
    },
  );

  const itemsSmart = (ops: IQueryOtions = { force: false }) => {
    if (!ignoreMounted) {
      onMounted(() => {
        if (ops.force || status.value.status === 'initial') {
          getDataAsync(ops);
        }
      });
    }
    return itemsDumb;
  };

  return { itemsDumb, itemsSmart, getDataAsync };
}
