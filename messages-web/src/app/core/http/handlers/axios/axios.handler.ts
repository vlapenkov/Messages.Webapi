import { createHandler } from '@/app/core/handlers/handler';
import { AxiosInstance, AxiosPromise } from 'axios';
import { http } from '../../http.service';

export type AxiosHandlerFunction<TResponse, TRequest = undefined> = (
  http: AxiosInstance,
  request: TRequest,
) => AxiosPromise<TResponse>;

/** Подставляет текущий экземпляр axios в произвольный запрос */
export const axiosHandler = createHandler(
  () =>
    <TResponse, TRequest = undefined>(handleRequest: AxiosHandlerFunction<TResponse, TRequest>) =>
    (request: TRequest) =>
      handleRequest(http, request),
);
