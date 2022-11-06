import { StateBase } from '../../base/state-base';

export const collectionProp = Symbol.for('--state--collection');

export const getCollectionProp = <T extends StateBase>(target: T) =>
  target[collectionProp as keyof T] as keyof T | undefined;
