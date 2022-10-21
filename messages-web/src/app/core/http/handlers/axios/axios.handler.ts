/* eslint-disable @typescript-eslint/no-explicit-any */
import { HandlerBase } from '@/app/core/cqrs/base/handler.base';
import { AxiosPromise } from 'axios';
import { http } from '../../http.service';
import { AxiosHandlerFunction } from './@types/AxiosHandlerFunction';

export class AxiosHandler<TOut = any, Tin = undefined> extends HandlerBase<
  AxiosPromise<TOut>,
  Tin
> {
  constructor(private fn: AxiosHandlerFunction<Tin, TOut>) {
    super();
  }

  handle(input: Tin): AxiosPromise<TOut> {
    return this.fn(http, input);
  }
}
