import { AxiosInstance, AxiosPromise } from 'axios';

export type AxiosHandlerFunction<Tin, TOut> = (arg: Tin, http: AxiosInstance) => AxiosPromise<TOut>;
