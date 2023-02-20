import { ModelBase } from '../base/model-base';
import { mockPropKey } from '../base/props-keys/mock.prop-key';

export const mock =
  <TVal>(mockFn: () => TVal) =>
  <TModel extends ModelBase>(target: TModel, key: string) => {
    Object.defineProperty(target, mockPropKey(key), {
      get: () => mockFn,
    });
  };
