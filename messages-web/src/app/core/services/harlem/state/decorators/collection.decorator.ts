import { StateBase } from '../base/state-base';
import { collectionProp } from './property-keys/collection.prop-key';

export const collection = <T extends StateBase>(target: T, key: keyof T) => {
  Object.defineProperty(target, collectionProp, {
    get: () => key,
  });
};
