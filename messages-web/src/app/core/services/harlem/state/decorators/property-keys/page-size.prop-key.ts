import { StateBase } from '../../base/state-base';

export const pageSizePropKey = Symbol('--state-page-size');

export const getPageSizeProp = <T extends StateBase>(target: T) =>
  target[pageSizePropKey as keyof T] as unknown as keyof T;
