import { ICommand } from '../cqrs/base/@types/ICommand';
import { IQuery } from '../cqrs/base/@types/IQuery';
import { command, query } from '../cqrs/cqrs.service';
import { HandlerBuilder } from '../cqrs/handler-builder';
import { AxiosPromiseUnwrapDecorator } from '../http/decorators/axios-promise-unwrap.decorator';
import { HttpFunction } from '../http/handlers/http/@types/HttpFunction';
import { UrlExtractor } from '../http/handlers/http/get-url-handler';
import { GetQuery } from '../http/handlers/http/get.wrapper';
import { PatchCommand } from '../http/handlers/http/patch.command';
import { PostCommand } from '../http/handlers/http/post.command';
import { PutCommand } from '../http/handlers/http/put.command';
import { ModelBase } from '../model/model-base';

export interface IMethodConfig {
  url: string;
}

export interface IRepositoryDefinitionOptional {
  url: string;
}

export interface ISetupContext<TModel extends ModelBase> {
  get<TResult = TModel, TInputArg extends IQuery<TResult> | undefined = undefined>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg>;
  post<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg>;
  put<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg>;
  patch<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg>;
  del<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg>;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface, @typescript-eslint/no-unused-vars
export interface IRepositoryDefinition<TModel extends ModelBase> {}

const defaultOptionalProps: IRepositoryDefinitionOptional = {
  url: '',
};

export function defineRepository<TModel extends ModelBase>(
  options: IRepositoryDefinition<TModel> & Partial<IRepositoryDefinitionOptional>,
) {
  const compiledOptions: IRepositoryDefinition<TModel> & IRepositoryDefinitionOptional = {
    ...defaultOptionalProps,
    ...options,
  };

  const buildUrlGetter =
    <TArg>(getter: UrlExtractor<TArg> = () => '') =>
    (arg?: TArg) =>
      compiledOptions.url + getter(arg);

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const defaultTransformer = <T extends HandlerBuilder<any>>(config: T) => {
    const wrapped = config.wrap((i) => new AxiosPromiseUnwrapDecorator(i));
    return wrapped;
  };

  function get<TResult = TModel, TInputArg extends IQuery<TResult> | undefined = undefined>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg> {
    const gq = new GetQuery<TResult, TInputArg>(buildUrlGetter(getUrl));
    const q = query(gq, (c) => {
      const auw = c.wrap((i) => {
        const dec = new AxiosPromiseUnwrapDecorator(i);
        return dec;
      });
      const res = defaultTransformer(c);
      return res;
    });
    return (arg: TInputArg) => q(arg);
  }

  function post<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg> {
    const cmd = command(
      new PostCommand<TResult, TInputArg>(buildUrlGetter(getUrl)),
      defaultTransformer,
    );
    return (arg) => cmd(arg);
  }

  function put<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg> {
    const cmd = command(
      new PutCommand<TResult, TInputArg>(buildUrlGetter(getUrl)),
      defaultTransformer,
    );
    return (arg) => cmd(arg);
  }

  function patch<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg> {
    const cmd = command(
      new PatchCommand<TResult, TInputArg>(buildUrlGetter(getUrl)),
      defaultTransformer,
    );
    return (arg) => cmd(arg);
  }

  function del<TResult = TModel, TInputArg extends ICommand<TResult> = ICommand<TResult>>(
    getUrl?: UrlExtractor<TInputArg>,
  ): HttpFunction<TResult, TInputArg> {
    const cmd = command(
      new PatchCommand<TResult, TInputArg>(buildUrlGetter(getUrl)),
      defaultTransformer,
    );
    return (arg) => cmd(arg);
  }

  const setupContext: ISetupContext<TModel> = {
    get,
    post,
    put,
    patch,
    del,
  };

  return setupContext;
}
