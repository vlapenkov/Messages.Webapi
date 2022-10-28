import { ModelBase } from '../base/model-base';
import { hiddenProp } from '../base/props/hidden.prop';

export const hidden = <T extends ModelBase>(target: T, key: string) => {
  Object.defineProperty(target, hiddenProp(key), {
    get: () => true,
  });
};
