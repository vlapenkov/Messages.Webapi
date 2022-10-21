import { AxiosInstance, AxiosPromise } from 'axios';

export type AxiosHandlerFunction<TOut, Tin = undefined> = (
  http: AxiosInstance,
  arg: Tin,
) => AxiosPromise<TOut>;
