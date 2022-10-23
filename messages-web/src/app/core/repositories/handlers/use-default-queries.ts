import { IModel } from '../../model/@types/IModel';
import { ModelBase } from '../../model/model-base';
import { IRepositoryQueries } from '../@types/IRepositoryQueries';

export function useDefaultQueries<TModel extends ModelBase<IModel>>({
  defineGet,
  definePost,
  definePut,
  definePatch,
  defineDelete,
}: IRepositoryQueries<TModel>) {
  return {
    get: defineGet(),
    post: definePost(),
    put: definePut(),
    patch: definePatch(),
    del: defineDelete(),
  };
}
