import { StateBase } from '../base/state-base';
import { pageNumberPropKey } from './property-keys/page-number.prop-key';

export const pageSize = <T extends StateBase>(target: T, key: string) => {
  Object.defineProperty(target, pageNumberPropKey, {
    get: () => key,
  });
};
