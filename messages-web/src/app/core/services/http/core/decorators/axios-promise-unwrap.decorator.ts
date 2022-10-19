import { AxiosPromise } from 'axios';
import { HandlerDecoratorBase } from '../../../cqrs/base/HandlerDecoratorBase';

export class AxiosPromiseUnwrapDecorator<Tin, TOut> extends HandlerDecoratorBase<
  Tin,
  Promise<TOut>,
  AxiosPromise<TOut>
> {
  async handle(input: Tin): Promise<TOut> {
    this.decorated.handle(input);
    throw new Error('Method not implemented.');
  }
}
