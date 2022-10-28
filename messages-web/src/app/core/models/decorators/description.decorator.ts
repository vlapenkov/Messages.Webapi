import { ModelBase } from '../base/model-base';
import { descriptonProp } from '../base/props/descripton.prop';

export const description =
  (descriptionValue: string) =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, descriptonProp(key), {
      get: () => descriptionValue,
    });
  };
