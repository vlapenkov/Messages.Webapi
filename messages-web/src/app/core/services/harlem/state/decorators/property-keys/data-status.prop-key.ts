import { StateBase } from '../../base/state-base';

export const dataStatusProp = Symbol.for('--state--data-status');

export const getDataStatusProp = <T extends StateBase>(target: T) =>
  target[dataStatusProp as keyof T] as keyof T | undefined;
