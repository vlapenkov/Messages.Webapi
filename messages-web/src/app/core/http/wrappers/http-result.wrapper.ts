import { AxiosPromise, HttpStatusCode } from 'axios';
import { ErrorResult } from '../results/error.result';
import { HttpResult } from '../results/base/http-result';
import { Ok } from '../results/ok.result';
import { createWrapper } from '../../handlers/handler-wrapper';
import { Handler } from '../../handlers/handler';

export function useHttpResult<TResponse, TRequest = undefined>() {
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
