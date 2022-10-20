/* eslint-disable @typescript-eslint/no-unused-vars */
import { IQuery } from '../cqrs/base/@types/IQuery';
import { query } from '../cqrs/cqrs.service';
import { AxiosPromiseUnwrapDecorator } from '../http/decorators/axios-promise-unwrap.decorator';
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

  // const get = <TArg extends IQuery<TModelClass>>(arg: TArg) => {
  //   const q = query(new GetQuery<TModelClass, TArg>(() => compiledOptions.url), (config) => {
  //     const wrapped = config.wrap((i) => {
  //       const x = new AxiosPromiseUnwrapDecorator(i);
  //       return x;
  //     });
  //     const xx = wrapped.handle();
  //     throw new Error('Not Implemented!');
  //   });
  // };
  compiledOptions.setup();
  throw new Error('Not Implemented!');
}
