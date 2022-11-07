import { IModel } from '@/app/core/models/@types/IModel';
import { IPagedRequest } from '../@types/IPagedRequest';
import { IPagedResponse } from '../@types/IPagedResponse';
import { defineHttpService, HttpServiceOptions } from '../define-http.service';
import { useDefaultQueries } from '../handlers/use-default-queries';

export function definePageableCollectionService<TIModel extends IModel>(opts: HttpServiceOptions) {
  const x = defineHttpService<TIModel>(opts);
  return {
    ...useDefaultQueries(x),
    get: x.defineGet<IPagedResponse<TIModel>, IPagedRequest>(),
  };
}
