import { StateBase } from '../base/state-base';
import { selectedItemProp, selectedItemPropOptions } from './property-keys/selected-item.prop-key';

export interface ISelectedItemOptions {
  create: boolean;
  update: boolean;
  delete: boolean;
}

const optsDefault: ISelectedItemOptions = {
  create: true,
  update: true,
  delete: true,
};

export const selected =
  (opts: Partial<ISelectedItemOptions> = {}) =>
  <T extends StateBase>(target: T, key: keyof T) => {
    const options: ISelectedItemOptions = { ...optsDefault, ...opts };
    Object.defineProperty(target, selectedItemProp, {
      get: () => key,
    });
    Object.defineProperty(target, selectedItemPropOptions(key as string), {
      get: () => options,
    });
  };
