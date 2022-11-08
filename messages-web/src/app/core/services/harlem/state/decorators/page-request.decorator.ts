import { StateBase } from '../base/state-base';
import { pageRequestpropKey } from './property-keys/page-request.prop-key';

export const pageRequest = <TState extends StateBase>(target: TState, key: keyof TState) => {
  Object.defineProperty(target, pageRequestpropKey, {
    get: () => key,
  });
};
