import { OptionsGetter } from '@/app/core/handlers/http/options/get-options.handler';
import { IModel } from '@/app/core/models/@types/IModel';
import type { IWrapperConfiguration } from '../define-http.service';
import { RequetstHandler } from './requetst-handler';

export interface IQueryConstructors<TIModel extends IModel> {
  defineGet: <TResponse = TIModel[], TRequest = void>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  definePost: <TResponse = TIModel, TRequest = TIModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  definePut: <TResponse = TIModel, TRequest = TIModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  definePatch: <TResponse = TIModel, TRequest = TIModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
  defineDelete: <TResponse = boolean, TRequest = TIModel>(
    config?: OptionsGetter<TRequest>,
    wrappersConfig?: IWrapperConfiguration,
  ) => RequetstHandler<TResponse, TRequest>;
}
