import { IModel, modelMarker } from '../@types/IModel';

export abstract class ModelBase<T extends IModel = IModel> implements IModel {
  [modelMarker]: never = null as never;

  abstract tryParse(model: T): boolean;

  abstract asObject(): T;

  abstract equals(mb: ModelBase): boolean;
}
