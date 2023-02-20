import { StateBase } from '../../base/state-base';

export const pageRequestpropKey = Symbol('--state-page-request');

export const getPageRequestProp = <T extends StateBase>(target: T) =>
  target[pageRequestpropKey as keyof T] as unknown as keyof T;
