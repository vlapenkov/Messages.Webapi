import { StateBase } from '../../base/state-base';

export const itemPropKey = Symbol('--state-single-item');

export const getItemKey = <TState extends StateBase>(target: TState) =>
  target[itemPropKey as keyof TState] as unknown as keyof TState;
