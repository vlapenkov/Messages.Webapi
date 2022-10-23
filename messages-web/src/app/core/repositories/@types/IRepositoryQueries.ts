import { OptionsGetter } from '../../http/handlers/options/get-options.handler';
import { ModelBase } from '../../model/model-base';
import { RequetstHandler } from './requetst-handler';

export interface IRepositoryQueries<TModel extends ModelBase> {
  defineGet: <TResponse = TModel[], TRequest = undefined>(
    config?: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  definePost: <TResponse = TModel, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  definePut: <TResponse = TModel, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  definePatch: <TResponse = TModel, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  defineDelete: <TResponse = boolean, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
}
