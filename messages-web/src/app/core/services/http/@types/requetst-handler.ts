import { HttpResult } from '@/app/core/handlers/http/results/base/http-result';
import { Handler } from '../../../handlers/base/handler';

export type RequetstHandler<TResponse, TRequest> = Handler<
  Promise<HttpResult<TResponse>>,
  TRequest
>;
