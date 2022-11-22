import { StateBase } from '../base/state-base';
import { triggerPropkeyFor } from './property-keys/trigger.prop-key';

export const trigger =
  <TState extends StateBase>(methodKey: string) =>
  (target: TState, key: string, _: PropertyDescriptor) => {
    Object.defineProperty(target, triggerPropkeyFor(key), {
      get: () => methodKey,
    });
  };
