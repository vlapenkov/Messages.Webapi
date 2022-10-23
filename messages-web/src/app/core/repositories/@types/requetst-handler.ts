import { Handler } from '../../handlers/handler';
import { HttpResult } from '../../http/results/base/http-result';

export type RequetstHandler<TResponse, TRequest> = Handler<
  Promise<HttpResult<TResponse>>,
  TRequest
>;
