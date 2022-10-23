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

export interface IMethodConfig {
  url: string;
}

export interface IRepositoryDefinitionOptional {
  url: string;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface, @typescript-eslint/no-unused-vars
export interface IRepositoryDefinition<TModel extends ModelBase> {}

const defaultOptionalProps: IRepositoryDefinitionOptional = {
  url: '',
};

export type QueryConstructor<TResponse, TRequest> = (
  config: OptionsGetter<TRequest>,
) => Handler<Promise<HttpResult<TResponse>>, TRequest>;

export function defineRepository<TModel extends ModelBase>(
  optionsProvided: IRepositoryDefinition<TModel> & Partial<IRepositoryDefinitionOptional>,
) {
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

  const get = <TResponse = TModel[], TRequest = undefined>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(useGet<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const post = <TResponse = TModel, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(usePost<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const put = <TResponse = TModel, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(usePut<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const patch = <TResponse = TModel, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(usePatch<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });
  const del = <TResponse = boolean, TRequest = TModel>(config: OptionsGetter<TRequest>) =>
    createHandler(() => {
      const getOptions = configureOptions(config);
      return extend(getOptions).wrap(useDelete<TResponse, TRequest>()).wrap(useHttpResult()).done();
    });

  return { get, post, put, patch, del };
}
