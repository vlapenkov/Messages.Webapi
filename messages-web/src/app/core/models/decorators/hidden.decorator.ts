import { ModelBase } from '../base/model-base';
import { hiddenPropkey } from '../base/props-keys/hidden.prop-key';
import { HiddenValue } from './HiddenValue';

export const hidden =
  (val: HiddenValue = 'always') =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, hiddenPropkey(key), {
      get: () => val,
    });
  };
