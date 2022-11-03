import { ModelBase } from '../base/model-base';
import { hiddenPropkey } from '../base/props-keys/hidden.prop-key';
import { DisplayMode } from './ViewMode';

export const hidden =
  (val: DisplayMode = 'always') =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, hiddenPropkey(key), {
      get: () => val,
    });
  };
