import { OptionsGetter } from '@/app/core/handlers/http/options/get-options.handler';
import { ModelBase } from '../../../models/model-base';
import type { IWrapperConfiguration } from '../define-http.service';
import { RequetstHandler } from './requetst-handler';

export interface IQueryConstructors<TModel extends ModelBase> {
  defineGet: <TResponse = TModel[], TRequest = void>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  definePost: <TResponse = TModel, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  definePut: <TResponse = TModel, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  definePatch: <TResponse = TModel, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  defineDelete: <TResponse = boolean, TRequest = TModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
}
