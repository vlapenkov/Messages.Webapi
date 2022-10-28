import { ModelBase } from '../base/model-base';

export const hidden = <T extends ModelBase>(target: T, key: string) => {
  Object.defineProperty(target, Symbol(`--hidden--${key}`), {
    get: () => true,
  });
};
