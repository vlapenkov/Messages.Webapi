import { HandlerBase } from '@/app/core/cqrs/base/HandlerBase';
import { AxiosPromise } from 'axios';
import { http } from '../../http.service';
import { AxiosHandlerFunction } from './@types/AxiosHandlerFunction';

export class AxiosHandler<Tin, TOut> extends HandlerBase<Tin, AxiosPromise<TOut>> {
  constructor(private fn: AxiosHandlerFunction<Tin, TOut>) {
    super();
  }

  handle(input: Tin): AxiosPromise<TOut> {
    return this.fn(input, http);
  }
}
