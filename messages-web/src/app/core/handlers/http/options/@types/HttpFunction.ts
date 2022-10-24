import { HttpResult } from '../../results/base/http-result';

export type HttpFunction<TResult, Tinput> = (arg?: Tinput) => Promise<HttpResult<TResult>>;
