import { StateBase } from '../base/state-base';
import { dataStatusProp } from './property-keys/data-status.prop-key';

export const dataStatus = <T extends StateBase>(target: T, key: keyof T) => {
  Object.defineProperty(target, dataStatusProp, {
    get: () => key,
  });
};
