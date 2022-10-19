import { IModel } from './@types/IModel';

export abstract class ModelBase<T extends IModel> implements IModel {
  abstract parseModel(model: T): void;
}
