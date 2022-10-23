import { createHandler, Handler } from '../handlers/handler';
import { extend } from '../handlers/handler-lab';
import { OptionsGetter, setAxiosOptonsFor } from '../http/handlers/options/get-options.handler';
import { HttpResult } from '../http/results/base/http-result';
import { useHttpResult } from '../http/wrappers/http-result.wrapper';
import {
  useDelete,
  useGet,
  usePatch,
  usePost,
  usePut,
} from '../http/wrappers/htttp-requests.wrappers';
import { ModelBase } from '../model/model-base';

export type RequetstHandler<TResponse, TRequest> = Handler<
  Promise<HttpResult<TResponse>>,
  TRequest
>;

export interface IRepositoryQueries<TModel extends ModelBase> {
  defineGet: <TResponse = TModel[], TRequest = undefined>(
    config: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  definePost: <TResponse = TModel, TRequest = TModel>(
    config: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  definePut: <TResponse = TModel, TRequest = TModel>(
    config: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  definePatch: <TResponse = TModel, TRequest = TModel>(
    config: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
  defineDelete: <TResponse = boolean, TRequest = TModel>(
    config: OptionsGetter<TRequest>,
  ) => RequetstHandler<TResponse, TRequest>;
}

export interface IRepositoryDefinitionOptional {
  url: string;
}

/** Обязательные параметры для репозитория. Пока не знаю какие */
// eslint-disable-next-line @typescript-eslint/no-empty-interface, @typescript-eslint/no-unused-vars
export interface IRepositoryDefinition<TModel extends ModelBase> {}

const defaultOptionalProps: IRepositoryDefinitionOptional = {
  url: '',
};

export function defineRepository<TModel extends ModelBase>(
  optionsProvided: IRepositoryDefinition<TModel> & Partial<IRepositoryDefinitionOptional>,
): IRepositoryQueries<TModel> {
  const repOptions: IRepositoryDefinition<TModel> & IRepositoryDefinitionOptional = {
    ...defaultOptionalProps,
    ...optionsProvided,
  };

  const configureOptions = <TRequest>(config: OptionsGetter<TRequest>) =>
    setAxiosOptonsFor<TRequest>((request) => {
      const queryOpts = config(request);
      queryOpts.url = repOptions.url + (queryOpts.url ?? '');
      return queryOpts;
    });

  const defineGet = <TResponse = TModel[], TRequest = undefined>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(useGet<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const definePost = <TResponse = TModel, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(usePost<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const definePut = <TResponse = TModel, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(usePut<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const definePatch = <TResponse = TModel, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(usePatch<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const defineDelete = <TResponse = boolean, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(useDelete<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });

  const context: IRepositoryQueries<TModel> = {
    defineGet,
    definePost,
    definePut,
    definePatch,
    defineDelete,
  };
  return context;
}
