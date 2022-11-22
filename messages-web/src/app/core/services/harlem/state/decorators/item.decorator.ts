import { StateBase } from '../base/state-base';
import { itemPropKey } from './property-keys/item.prop-key';

export const item = <TState extends StateBase>(target: TState, key: keyof TState) => {
  Object.defineProperty(target, itemPropKey, {
    get: () => key,
  });
};
