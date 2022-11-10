import { Constructor } from '@/app/@types/constructor';
import { createDefaultStore, DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';

export class StoreCreationContext<TState extends StateBase> {
  constructor(public state: TState, public store: DefaultStore<TState>) {}
}

export const createContext = <TState extends StateBase>(
  name: string,
  State: Constructor<TState>,
): StoreCreationContext<TState> => {
  const state = new State();
  const store: DefaultStore<TState> = createDefaultStore(name, state);
  return new StoreCreationContext(state, store);
};
