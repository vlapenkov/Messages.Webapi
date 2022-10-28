import { ModelBase } from '../base/model-base';

export const description =
  (descriptionValue: string) =>
  <T extends ModelBase>(target: T, key: string) => {
    Object.defineProperty(target, Symbol(`--description-${key}`), {
      get: () => descriptionValue,
    });
  };
