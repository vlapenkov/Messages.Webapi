import { AxiosInstance, AxiosResponse } from 'axios';

export type AxiosHandlerFunction<Tin, TOut, D> = (
  arg: Tin,
  http: AxiosInstance,
) => Promise<AxiosResponse<TOut, D>>;
