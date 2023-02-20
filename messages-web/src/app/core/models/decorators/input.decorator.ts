import { InputType } from '../base/@types/input-type';
import { ModelBase } from '../base/model-base';
import { inputPropKeyFor } from '../base/props-keys/iput.prop-key';

export const input =
  <TModel extends ModelBase>(t: InputType) =>
  (target: TModel, key: string) => {
    Object.defineProperty(target, inputPropKeyFor(key), {
      get: () => t,
    });
  };
