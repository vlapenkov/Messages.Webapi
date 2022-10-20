import { IModel, modelMarker } from './@types/IModel';

export abstract class ModelBase<T extends IModel> implements IModel {
  [modelMarker]: never = null as never;

  abstract parseModel(model: T): void;

  abstract asObject(): T;
}
