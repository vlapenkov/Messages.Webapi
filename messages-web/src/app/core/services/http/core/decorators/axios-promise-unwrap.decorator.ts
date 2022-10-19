import { AxiosPromise } from 'axios';
import { HandlerDecoratorBase } from '../../../cqrs/base/HandlerDecoratorBase';

export class AxiosPromiseUnwrapDecorator<Tin, TOut> extends HandlerDecoratorBase<
  Tin,
  TOut,
  AxiosPromise<TOut>
> {
  handle(input: Tin): TOut {
    this.decorated.handle(input);
    throw new Error('Method not implemented.');
  }
}
