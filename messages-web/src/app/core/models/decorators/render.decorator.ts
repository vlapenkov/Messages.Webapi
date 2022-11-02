import { VNodeChild } from 'vue';
import { ModelBase } from '../base/model-base';
import { renderPropkey } from '../base/props-keys/render.prop-key';

export const render =
  <TModel extends ModelBase>(func: (model: TModel) => VNodeChild, mode = 'default') =>
  (target: TModel, key: string) => {
    Object.defineProperty(target, renderPropkey(key, mode), {
      get() {
        return (m: TModel) => () => func(m);
      },
    });
  };
