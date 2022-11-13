import { ColumnProps } from 'primevue/column';
import { ModelBase } from '../base/model-base';
import { columnKeyFor } from '../base/props-keys/column.prop-key';

export const column =
  <TModel extends ModelBase>(func: (model: TModel) => ColumnProps) =>
  (target: TModel, key: string) => {
    Object.defineProperty(target, columnKeyFor(key), {
      get: () => func,
    });
  };
