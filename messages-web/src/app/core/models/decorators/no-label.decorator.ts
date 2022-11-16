import { ModelBase } from '../base/model-base';
import { nolabelPropKeyFor } from '../base/props-keys/no-label.prop-key';

export const noLabel = <TModel extends ModelBase>(target: TModel, key: string) => {
  Object.defineProperty(target, nolabelPropKeyFor(key), {
    get: () => true,
  });
};
