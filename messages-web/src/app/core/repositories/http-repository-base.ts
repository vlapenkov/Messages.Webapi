/* eslint-disable @typescript-eslint/no-unused-vars */
import { IQuery } from '../cqrs/base/@types/IQuery';
import { HandlerTransformer } from '../cqrs/base/@types/transformers';
import { HandlerDecoratorBase } from '../cqrs/base/handler-decorator.base';
import { query } from '../cqrs/cqrs.service';
import { HandlerBuilder } from '../cqrs/handler-builder';
import { AxiosPromiseUnwrapDecorator } from '../http/decorators/axios-promise-unwrap.decorator';
import { AxiosHandler } from '../http/handlers/axios/axios.handler';
import { HttpFunction } from '../http/handlers/http/@types/HttpFunction';
import { UrlGetter } from '../http/handlers/http/@types/UrlGetter';
import { GetQuery } from '../http/handlers/http/get.query';
import { IModel } from '../model/@types/IModel';
import { ModelBase } from '../model/model-base';

export interface IMethodConfig {
  url: string;
}

export interface IRepositoryDefinitionOptional {
  url: string;
}

export interface IRepositoryDefinition {
  setup: () => void;
}

const defaultOptionalProps: IRepositoryDefinitionOptional = {
  url: '',
};

export function defineRepository<TModel extends IModel, TModelClass extends ModelBase<TModel>>(
  options: IRepositoryDefinition & Partial<IRepositoryDefinitionOptional>,
) {
  const compiledOptions: IRepositoryDefinition & IRepositoryDefinitionOptional = {
    ...defaultOptionalProps,
    ...options,
  };

  const buildUrlGetter =
    <TArg>(getter: UrlGetter<TArg> = () => '') =>
    (arg: TArg) =>
      compiledOptions.url + getter(arg);

  const defaultTransformer = <T extends AxiosHandler>(config: HandlerBuilder<T>) => {
    const wrapped = config.wrap((i) => new AxiosPromiseUnwrapDecorator(i));
    return wrapped;
  };

  const get = <TResult = TModelClass, TArg extends IQuery<TResult> = IQuery<TResult>>(
    getUrl?: UrlGetter<TArg>,
  ): HttpFunction<TResult, TArg> => {
    const q = query(new GetQuery<TResult, TArg>(buildUrlGetter(getUrl)), defaultTransformer);
    return (arg: TArg) => q(arg);
  };

  compiledOptions.setup();
  throw new Error('Not Implemented!');
}
