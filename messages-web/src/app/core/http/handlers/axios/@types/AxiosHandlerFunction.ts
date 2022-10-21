import { AxiosInstance, AxiosPromise } from 'axios';

export type AxiosHandlerFunction<Tin, TOut> = (
  http: AxiosInstance,
  arg?: Tin,
) => AxiosPromise<TOut>;
