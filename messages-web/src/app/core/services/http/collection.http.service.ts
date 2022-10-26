import { IModel } from '../../models/@types/IModel';
import { RequetstHandler } from './@types/requetst-handler';
import { defineHttpService, HttpServiceOptions } from './define-http.service';
import { useDefaultQueries } from './handlers/use-default-queries';

export interface ICollectionHttpService<TIModel extends IModel> {
  get: RequetstHandler<TIModel[], void>;
  post: RequetstHandler<TIModel, TIModel>;
  put: RequetstHandler<TIModel, TIModel>;
  patch: RequetstHandler<TIModel, TIModel>;
  del: RequetstHandler<boolean, TIModel>;
}

export function defineCollectionService<TIModel extends IModel>(
  opts: HttpServiceOptions,
): ICollectionHttpService<TIModel> {
  return useDefaultQueries(defineHttpService<TIModel>(opts));
}
