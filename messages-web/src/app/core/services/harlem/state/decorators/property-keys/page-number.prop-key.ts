import { StateBase } from '../../base/state-base';

export const pageNumberPropKey = Symbol('--state-page-number');

export const getPageNumberProp = <T extends StateBase>(target: T) =>
  target[pageNumberPropKey as keyof T];
