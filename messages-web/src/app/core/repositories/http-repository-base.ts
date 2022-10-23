import { createHandler } from '../handlers/handler';
import { extend } from '../handlers/handler-lab';
import { OptionsGetter, setAxiosOptonsFor } from '../http/handlers/options/get-options.handler';
import { useHttpResult } from '../http/wrappers/http-result.wrapper';
import {
  useDelete,
  useGet,
  usePatch,
  usePost,
  usePut,
} from '../http/wrappers/htttp-requests.wrappers';
import { ModelBase } from '../model/model-base';
import { IRepositoryQueries } from './@types/IRepositoryQueries';

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

  const defineGet = <TResponse = TModel[], TRequest = undefined>(
    config: OptionsGetter<TRequest> = () => ({}),
  ) =>
    createHandler(() =>
      extend(configureOptions(config))
        .wrap(useGet<TResponse, TRequest>())
        .wrap(useHttpResult())
        .done(),
    );
  const definePost = <TResponse = TModel, TRequest = TModel>(
    config: OptionsGetter<TRequest> = () => ({}),
  ) =>
    createHandler(() =>
      extend(configureOptions(config))
        .wrap(usePost<TResponse, TRequest>())
        .wrap(useHttpResult())
        .done(),
    );
  const definePut = <TResponse = TModel, TRequest = TModel>(
    config: OptionsGetter<TRequest> = () => ({}),
  ) =>
    createHandler(() =>
      extend(configureOptions(config))
        .wrap(usePut<TResponse, TRequest>())
        .wrap(useHttpResult())
        .done(),
    );
  const definePatch = <TResponse = TModel, TRequest = TModel>(
    config: OptionsGetter<TRequest> = () => ({}),
  ) =>
    createHandler(() =>
      extend(configureOptions(config))
        .wrap(usePatch<TResponse, TRequest>())
        .wrap(useHttpResult())
        .done(),
    );
  const defineDelete = <TResponse = boolean, TRequest = TModel>(
    config: OptionsGetter<TRequest> = () => ({}),
  ) =>
    createHandler(() =>
      extend(configureOptions(config))
        .wrap(useDelete<TResponse, TRequest>())
        .wrap(useHttpResult())
        .done(),
    );

  const context: IRepositoryQueries<TModel> = {
    defineGet,
    definePost,
    definePut,
    definePatch,
    defineDelete,
  };
  return context;
}
