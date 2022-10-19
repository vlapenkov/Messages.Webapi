import { HandlerBase } from '@/app/core/services/cqrs/base/HandlerBase';
import { AxiosResponse } from 'axios';
import { http } from '../../http.service';
import { AxiosHandlerFunction } from './@types/AxiosHandlerFunction';

export class AxiosHandler<Tin, TOut, D> extends HandlerBase<Tin, Promise<AxiosResponse<TOut, D>>> {
  constructor(private fn: AxiosHandlerFunction<Tin, TOut, D>) {
    super();
  }

  handle(input: Tin): Promise<AxiosResponse<TOut, D>> {
    return this.fn(input, http);
  }
}
