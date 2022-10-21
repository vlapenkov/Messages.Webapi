import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { CommandBase } from '@/app/core/cqrs/base/command.base';
import { AxiosPromise } from 'axios';
import { http } from '../../http.service';
import { AxiosHandlerFunction } from './@types/AxiosHandlerFunction';

export class AxiosCommand<TOut, Tin extends ICommand<TOut>> extends CommandBase<
  AxiosPromise<TOut>,
  Tin
> {
  constructor(private fn: AxiosHandlerFunction<Tin, TOut>) {
    super();
  }

  handle(input?: Tin): AxiosPromise<TOut> {
    return this.fn(http, input);
  }
}
