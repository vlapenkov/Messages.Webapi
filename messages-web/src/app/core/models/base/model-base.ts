import { IModel } from '../@types/IModel';

export abstract class ModelBase<T extends IModel = IModel> {
  abstract tryParseModel(model: T): boolean;

  abstract asObject(): T;

  abstract equals(mb: ModelBase): boolean;
}
