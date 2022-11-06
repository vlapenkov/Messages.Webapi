import { StateBase } from '../../base/state-base';

export const selectedItemProp = Symbol.for('--state--selected-item');

export const selectedItemPropOptions = (key: string) =>
  Symbol.for(`--selected-item-options--${key}`);

export const getSelectedItemPropKey = <T extends StateBase>(target: T) =>
  target[selectedItemProp as keyof T] as unknown as keyof T | undefined;

export const getSelectedItemPropOptions = <T extends StateBase>(key: string, target: T) =>
  target[selectedItemPropOptions(key) as keyof T] as unknown as keyof T | undefined;
