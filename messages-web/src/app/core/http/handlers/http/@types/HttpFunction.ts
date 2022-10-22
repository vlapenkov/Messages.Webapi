import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { IQuery } from '@/app/core/cqrs/base/@types/IQuery';
import { HttpResult } from '../../../results/base/http-result';
import { UrlExtractor } from '../get-url-handler';

export type HttpFunction<TResult, Tinput> = (arg?: Tinput) => Promise<HttpResult<TResult>>;

export type HttpQueryGeneric = <
  TQueryResult,
  Tinput extends IQuery<TQueryResult> = IQuery<TQueryResult>,
>(
  arg: Tinput,
) => Promise<HttpResult<TQueryResult>>;

export type HttpCommandGeneric = <
  TCommandResult,
  Tinput extends ICommand<TCommandResult> = ICommand<TCommandResult>,
>(
  arg: Tinput,
) => Promise<HttpResult<TCommandResult>>;

export type HttpFunctionFactory<TResult, Tinput> = (
  getUrl: UrlExtractor<Tinput>,
) => HttpFunction<Tinput, TResult>;
