import { ModelBase } from '../base/model-base';
import { descriptonPropkey } from '../base/props-keys/descripton.prop-key';

export const description =
  (descriptionValue: string) =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, descriptonPropkey(key), {
      get: () => descriptionValue,
    });
  };
