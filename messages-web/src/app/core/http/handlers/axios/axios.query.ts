import { IQuery } from '@/app/core/cqrs/base/@types/IQuery';
import { QueryBase } from '@/app/core/cqrs/base/query.base';
import { AxiosPromise } from 'axios';
import { http } from '../../http.service';
import { AxiosHandlerFunction } from './@types/AxiosHandlerFunction';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export class AxiosQuery<TOut, Tin extends IQuery<TOut> | undefined = undefined> extends QueryBase<
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
