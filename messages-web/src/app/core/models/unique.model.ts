import { IModelUnique } from './@types/IModel';
import { ModelBase } from './base/model-base';

export abstract class UniqueModel<
  TId extends string | number | symbol,
  TModel extends IModelUnique<TId>,
> extends ModelBase<TModel> {
  abstract id: TId;

  compareId(other: UniqueModel<TId, TModel>) {
    return this.id === other.id;
  }
}
