/* eslint-disable @typescript-eslint/no-unused-vars */
import { IQuery } from '../cqrs/base/@types/IQuery';
import { HandlerDecoratorBase } from '../cqrs/base/handler-decorator.base';
import { query } from '../cqrs/cqrs.service';
import { AxiosPromiseUnwrapDecorator } from '../http/decorators/axios-promise-unwrap.decorator';
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

  const get = <TArg extends IQuery<TModelClass>>(getUrl?: UrlGetter<TArg>) => {
    const q = query(new GetQuery<TModelClass, TArg>(buildUrlGetter(getUrl)), (config) => {
      const wrapped = config.wrap((i) => new AxiosPromiseUnwrapDecorator(i));
      return wrapped;
    });
    return (arg: TArg) => q(arg);
  };
  compiledOptions.setup();
  throw new Error('Not Implemented!');
}
