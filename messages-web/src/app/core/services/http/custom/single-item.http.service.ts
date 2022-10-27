import { IModel } from '../../../models/@types/IModel';
import { RequetstHandler } from '../@types/requetst-handler';
import { HttpServiceOptions, defineHttpService } from '../define-http.service';
import { useDefaultQueries } from '../handlers/use-default-queries';

export interface ISingleItemHttpService<TIModel extends IModel> {
  get: RequetstHandler<TIModel, void>;
  post: RequetstHandler<TIModel, TIModel>;
  put: RequetstHandler<TIModel, TIModel>;
  patch: RequetstHandler<TIModel, TIModel>;
  del: RequetstHandler<boolean, TIModel>;
}

export function defineSingleItemService<TIModel extends IModel>(
  opts: HttpServiceOptions,
): ISingleItemHttpService<TIModel> {
  const x = defineHttpService<TIModel>(opts);
  return {
    ...useDefaultQueries(x),
    get: x.defineGet<TIModel>(),
  };
}
