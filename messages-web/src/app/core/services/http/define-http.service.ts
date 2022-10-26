import { AxiosPromise } from 'axios';
import { createHandler, Handler } from '../../handlers/base/handler';
import { extend } from '../../handlers/base/handler-lab';
import { HandlerDecorator, HandlerWrapper } from '../../handlers/base/handler-wrapper';
import { OptionsGetter, setAxiosOptonsFor } from '../../handlers/http/options/get-options.handler';
import {
  useDelete,
  useGet,
  usePatch,
  usePost,
  usePut,
} from '../../handlers/http/queries/htttp-queries.wrappers';
import { useHttpResult } from '../../handlers/http/results/http-result.wrapper';
import { ModelBase } from '../../models/model-base';
import { IQueryConstructors } from './@types/IRepositoryQueries';
import { AnyRequest } from './@types/requetst-handler';

export interface IWrapperConfiguration {
  append?: HandlerDecorator<AnyRequest>[];
  override?: HandlerDecorator<AnyRequest>[];
}

export interface IServiceOptionsOptional {
  url: string;
  wrappers: IWrapperConfiguration;
}

/** Обязательные параметры для репозитория. Пока не знаю какие */
// eslint-disable-next-line @typescript-eslint/no-empty-interface, @typescript-eslint/no-unused-vars
export interface IServiceOptionsRequired<TModel extends ModelBase> {}

const defaultOptionalProps: IServiceOptionsOptional = {
  url: '',
  wrappers: {},
};

export function defineHttpService<TModel extends ModelBase>(
  optionsProvided: IServiceOptionsRequired<TModel> & Partial<IServiceOptionsOptional>,
): IQueryConstructors<TModel> {
  const repOptions: IServiceOptionsRequired<TModel> & IServiceOptionsOptional = {
    ...defaultOptionalProps,
    ...optionsProvided,
  };

  const defaultRepoWrappers: HandlerDecorator<AnyRequest>[] = [];
  const repoWrappers = [
    ...(repOptions.wrappers.override ?? defaultRepoWrappers),
    ...(repOptions.wrappers.append ?? []),
  ];

  const configureOptions = <TRequest>(config: OptionsGetter<TRequest>) =>
    setAxiosOptonsFor<TRequest>((request) => {
      const queryOpts = config(request);
      queryOpts.url = repOptions.url + (queryOpts.url ?? '');
      return queryOpts;
    });

  const configureWrappers = (wrappers: IWrapperConfiguration) => [
    ...(wrappers.override ?? repoWrappers),
    ...(wrappers.append ?? []),
  ];

  function defineQueryGetter<TResponse = TModel[], TRequest = void>(
    queryWrapper: HandlerWrapper<
      OptionsGetter<TRequest>,
      Handler<AxiosPromise<TResponse>, TRequest>
    >,
  ) {
    return (
      config: OptionsGetter<TRequest> = () => ({}),
      wrappersConfig: IWrapperConfiguration = {},
    ) => {
      const handler = createHandler(() =>
        extend(configureOptions(config)).wrap(queryWrapper).wrap(useHttpResult()).done(),
      );
      const wrappers = configureWrappers(wrappersConfig);
      return extend(handler)
        .wrapMany(...wrappers)
        .done();
    };
  }

  const context: IQueryConstructors<TModel> = {
    defineGet: defineQueryGetter(useGet()),
    definePost: defineQueryGetter(usePost()),
    definePut: defineQueryGetter(usePut()),
    definePatch: defineQueryGetter(usePatch()),
    defineDelete: defineQueryGetter(useDelete()),
  };
  return context;
}
