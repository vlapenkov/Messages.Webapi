import { ModelBase } from '../base/model-base';
import { hiddenPropkey } from '../base/props-keys/hidden.prop-key';

export const hidden = <T extends ModelBase>(target: T, key: string) => {
  Object.defineProperty(target, hiddenPropkey(key), {
    get: () => true,
  });
};
