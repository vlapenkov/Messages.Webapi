import { VNodeChild } from 'vue';
import { ModelBase } from '../base/model-base';
import { renderPropkey } from '../base/props-keys/render.prop-key';
import { DisplayMode } from './@types/ViewMode';

export const render =
  <TModel extends ModelBase>(func: (model: TModel) => VNodeChild, mode: DisplayMode = 'view') =>
  (target: TModel, key: string) => {
    Object.defineProperty(target, renderPropkey(key, mode), {
      get() {
        return (m: TModel) => () => func(m);
      },
    });
  };
