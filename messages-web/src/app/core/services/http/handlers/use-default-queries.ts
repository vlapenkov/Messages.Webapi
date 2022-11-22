import { IModel } from '../../../models/@types/IModel';
import { IQueryConstructors } from '../@types/IRepositoryQueries';

export function useDefaultQueries<TIModel extends IModel>({
  defineGet,
  definePost,
  definePut,
  definePatch,
  defineDelete,
}: IQueryConstructors<TIModel>) {
  return {
    get: defineGet(),
    post: definePost(),
    put: definePut(),
    patch: definePatch(),
    del: defineDelete(),
  };
}
