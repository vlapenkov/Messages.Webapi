import { IModelUnique } from './@types/IModel';
import { ModelBase } from './base/model-base';

export abstract class UniqueModel<
  TId extends string | number | symbol,
  TModel extends IModelUnique<TId>,
> extends ModelBase<TModel> {
  abstract id: TId;

  get key() {
    return this.id;
  }
}
