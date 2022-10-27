import { Constructor } from '@/app/@types/constructor';
import { extend } from '@/app/core/handlers/base/handler-lab';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { computedAsync } from '@vueuse/core';
import { ICollectionHttpService } from '../../../http/custom/collection.http-service';
import { parseArray } from '../../../http/handlers/parse.handlers';
import { DataStatus } from '../../base/tools/data-status';
import { DataTarget } from '../../base/tools/data-target';
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
  const { getter, mutation, computeState, action } = store;
  const getData = getter('get-data', (state) => state.dataSelected ?? state.dataDefault);
  const setData = mutation<{ data: TModel[]; target?: DataTarget }>(
    'set-data',
    (state, payload) => {
      const target: DataTarget = payload.target ?? 'selected';
      return target === 'default' ? state.dataDefault : state.dataSelected;
    },
  );
  const loadingStatus = computeState((state) => state.status);

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
        setData({ data: response.data ?? [], target: 'default' });
        loadingStatus.value = new DataStatus('loaded');
      } else {
        loadingStatus.value = new DataStatus('error', response.message);
      }
      return getData.value;
    },
  );

  const getDataAsync = (ops: { force: boolean } = { force: false }) =>
    computedAsync(() => getDataAsyncAction(ops), null);

  const extended = { getData, setData, loadingStatus, getDataAsync } as const;

  return [store, extended] as const;
}
