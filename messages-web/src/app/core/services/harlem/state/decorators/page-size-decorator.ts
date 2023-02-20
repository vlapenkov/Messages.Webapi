import { StateBase } from '../base/state-base';
import { pageSizePropKey } from './property-keys/page-size.prop-key';

export const pageSize = <T extends StateBase>(target: T, key: string) => {
  Object.defineProperty(target, pageSizePropKey, {
    get: () => key,
  });
};
