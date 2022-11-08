import { IModel } from '@/app/core/models/@types/IModel';
import { IPagedRequest } from '../@types/IPagedRequest';
import { IPagedResponse } from '../@types/IPagedResponse';
import { IQueryConstructors } from '../@types/IRepositoryQueries';
import { RequetstHandler } from '../@types/requetst-handler';
import { defineHttpService, HttpServiceOptions } from '../define-http.service';
import { useDefaultQueries } from '../handlers/use-default-queries';

export interface IPageableCollectionHttpServie<TIModel extends IModel> {
  post: RequetstHandler<TIModel, TIModel>;
  put: RequetstHandler<TIModel, TIModel>;
  getPage: RequetstHandler<IPagedResponse<TIModel>, IPagedRequest>;
}

export function definePageableCollectionService<TIModel extends IModel>(
  opts: HttpServiceOptions,
): [IPageableCollectionHttpServie<TIModel>, IQueryConstructors<TIModel>] {
  const queryCtors = defineHttpService<TIModel>(opts);
  const { post, put } = useDefaultQueries(queryCtors);
  return [
    {
      post,
      put,
      getPage: queryCtors.defineGet<IPagedResponse<TIModel>, IPagedRequest>(),
    },
    queryCtors,
  ];
}
