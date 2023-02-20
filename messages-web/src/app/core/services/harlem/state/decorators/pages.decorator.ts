import { StateBase } from '../base/state-base';
import { pagesPropKey } from './property-keys/pages.prop-key';

export const pages = <T extends StateBase>(target: T, key: string) => {
  Object.defineProperty(target, pagesPropKey, {
    get: () => key,
  });
};
