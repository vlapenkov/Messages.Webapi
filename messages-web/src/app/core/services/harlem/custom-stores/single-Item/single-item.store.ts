import { Constructor } from '@/app/@types/constructor';
import { extend } from '@/app/core/handlers/base/handler-lab';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { onMounted } from 'vue';
import { ISingleHttpService } from '../../../http/custom/single.http-service';
import { parse } from '../../../http/handlers/parse.handlers';
import { DefaultStore, createDefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getDataStatusProp } from '../../state/decorators/property-keys/data-status.prop-key';
import { getIgnoreMountedOpt } from '../../state/decorators/property-keys/ignore-mounted.prop-key';
import { getItemKey } from '../../state/decorators/property-keys/item.prop-key';
import { DataStatus } from '../../tools/data-status';
import { IQueryOtions } from '../tools/@types/IQueryOptions';
import { useSelectedItemForSingle } from '../tools/useSelectedItem';
import { useTriggers } from '../tools/useTriggers';
import { ISingleItemStore } from './@types/ISingleItemStore';

export function defineSingleItemStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends StateBase,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<TState>,
  service: ISingleHttpService<TIModel>,
): [ISingleItemStore<TModel>, DefaultStore<TState>] {
  const stateDefault = new State();
  const store: DefaultStore<TState> = createDefaultStore(name, stateDefault);
  const { computeState, action } = store;

  const itemKey = getItemKey(stateDefault);
  if (itemKey == null) {
    throw new Error('Please? provide @item in item store');
  }

  const ignoreMounted = getIgnoreMountedOpt(store.state, itemKey as string) ?? false;

  const item = computeState((state) => state[itemKey] as unknown as TModel);

  const dataStatusKey = getDataStatusProp(stateDefault);

  if (dataStatusKey == null) {
    throw new Error('Please, provide @dataStatus for collection state');
  }

  const status = computeState((state) => state[dataStatusKey] as unknown as DataStatus);

  const getDataAsync: Action<IQueryOtions & { arg?: Record<string, string | number> }, TModel> =
    action('get-item-async', async (ops = { force: false }) => {
      const currentStatus = status.value.status;
      if (!ops.force && currentStatus === 'loaded') {
        return item.value;
      }

      status.value = new DataStatus(currentStatus === 'initial' ? 'loading' : 'updating');
      const requestFn = extend(service.get).pipe(parse(Model)).done();
      const response = await requestFn(ops.arg as unknown as void);
      if (response.status === HttpStatus.Success) {
        if (response.data != null) {
          item.value = response.data;
        }
        status.value = new DataStatus('loaded');
      } else {
        status.value = new DataStatus('error', response.message);
      }

      return item.value;
    });

  const itemSmart = (ops: IQueryOtions = { force: false }) => {
    if (!ignoreMounted) {
      onMounted(() => {
        if (ops.force || status.value.status === 'initial') {
          getDataAsync(ops);
        }
      });
    }
    return item;
  };

  useTriggers(store, { getDataAsync });

  const { createItem, itemSelected, saveChanges, selectItem } = useSelectedItemForSingle(
    store,
    Model,
    service,
    item,
  );

  return [
    {
      itemSmart,
      status,
      createItem,
      itemSelected,
      saveChanges,
      selectItem,
    },
    store,
  ];
}
