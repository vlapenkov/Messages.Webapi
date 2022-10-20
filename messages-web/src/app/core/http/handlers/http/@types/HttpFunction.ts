import { HttpResult } from '../../../results/base/http-result';
import { UrlGetter } from './UrlGetter';

export type HttpFunction<TResult, Tinput> = (arg: Tinput) => Promise<HttpResult<TResult>>;

export type HttpFunctionFactory<TResult, Tinput> = (
  getUrl: UrlGetter<Tinput>,
) => HttpFunction<Tinput, TResult>;
