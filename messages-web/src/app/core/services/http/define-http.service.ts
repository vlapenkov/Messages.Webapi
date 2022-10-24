import { createHandler } from '../../handlers/base/handler';
import { extend } from '../../handlers/base/handler-lab';
import { OptionsGetter, setAxiosOptonsFor } from '../../handlers/http/options/get-options.handler';
import {
  useDelete,
  useGet,
  usePatch,
  usePost,
  usePut,
} from '../../handlers/http/queries/htttp-queries.wrappers';
import { useHttpResult } from '../../handlers/http/results/http-result.wrapper';
import { ModelBase } from '../../model/model-base';
import { IQueryConstructors } from './@types/IRepositoryQueries';

export interface IServiceOptionsOptional {
  url: string;
}

/** Обязательные параметры для репозитория. Пока не знаю какие */
// eslint-disable-next-line @typescript-eslint/no-empty-interface, @typescript-eslint/no-unused-vars
export interface IServiceOptionsRequired<TModel extends ModelBase> {}

const defaultOptionalProps: IServiceOptionsOptional = {
  url: '',
};

export function defineHttpService<TModel extends ModelBase>(
  optionsProvided: IServiceOptionsRequired<TModel> & Partial<IServiceOptionsOptional>,
): IQueryConstructors<TModel> {
  const repOptions: IServiceOptionsRequired<TModel> & IServiceOptionsOptional = {
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

  const context: IQueryConstructors<TModel> = {
    defineGet,
    definePost,
    definePut,
    definePatch,
    defineDelete,
  };
  return context;
}
