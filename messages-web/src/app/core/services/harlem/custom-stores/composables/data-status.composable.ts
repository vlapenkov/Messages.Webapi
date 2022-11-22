import { DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getDataStatusProp } from '../../state/decorators/property-keys/data-status.prop-key';
import { DataStatus } from '../../tools/data-status';

export function useDataStatus<TState extends StateBase>(store: DefaultStore<TState>) {
  const dataStatusKey = getDataStatusProp(store.state);

  if (dataStatusKey == null) {
    throw new Error('Please, provide @dataStatus for collection state');
  }

  const status = store.computeState(
    (state) => state[dataStatusKey as keyof TState] as unknown as DataStatus,
  );
  return status;
}
