import { StateBase } from '../base/state-base';
import { ignoreMountedPropKey } from './property-keys/ignore-mounted.prop-key';

export const ignoreMounted = <TState extends StateBase>(target: TState, key: string) => {
  Object.defineProperty(target, ignoreMountedPropKey(key), {
    get: () => true,
  });
};
