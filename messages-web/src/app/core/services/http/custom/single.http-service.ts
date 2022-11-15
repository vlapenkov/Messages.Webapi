import { IModel } from '../../../models/@types/IModel';
import { IQueryConstructors } from '../@types/IRepositoryQueries';
import { RequetstHandler } from '../@types/requetst-handler';
import { HttpServiceOptions, defineHttpService } from '../define-http.service';
import { useDefaultQueries } from '../handlers/use-default-queries';

export interface ISingleHttpService<TIModel extends IModel> {
  get: RequetstHandler<TIModel, void>;
  post: RequetstHandler<TIModel, TIModel>;
  put: RequetstHandler<TIModel, TIModel>;
  patch: RequetstHandler<TIModel, TIModel>;
  del: RequetstHandler<boolean, TIModel>;
}

export function defineSingleHttpService<TIModel extends IModel>(opts: HttpServiceOptions) {
  const x = defineHttpService<TIModel>(opts);
  const service = {
    ...useDefaultQueries(x),
    get: x.defineGet<TIModel>(),
  };
  return [service, x] as [ISingleHttpService<TIModel>, IQueryConstructors<TIModel>];
}
