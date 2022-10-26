import { AxiosPromise, HttpStatusCode } from 'axios';
import { ErrorResult } from './error.result';
import { HttpResult } from './base/http-result';
import { Ok } from './ok.result';
import { Handler } from '../../base/handler';
import { createWrapper } from '../../base/handler-wrapper';

/** Заворачивает респонс от Axios-а в обобщённый результат */
export function useHttpResult<TResponse, TRequest = void>() {
  return createWrapper<
    Handler<AxiosPromise<TResponse>, TRequest>,
    Handler<Promise<HttpResult<TResponse>>, TRequest>
  >((wrapped) => async (request) => {
    const response = await wrapped(request);
    if (response.status === HttpStatusCode.Ok) {
      return new Ok(response.data);
    }
    return new ErrorResult<TResponse>(response.statusText);
  });
}
