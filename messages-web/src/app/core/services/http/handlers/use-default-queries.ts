import { IModel } from '../../../models/@types/IModel';
import { ModelBase } from '../../../models/model-base';
import { IQueryConstructors } from '../@types/IRepositoryQueries';

export function useDefaultQueries<TModel extends ModelBase<IModel>>({
  defineGet,
  definePost,
  definePut,
  definePatch,
  defineDelete,
}: IQueryConstructors<TModel>) {
  return {
    get: defineGet(),
    post: definePost(),
    put: definePut(),
    patch: definePatch(),
    del: defineDelete(),
  };
}
