import { AxiosPromise, HttpStatusCode } from 'axios';
import { HandlerDecoratorBase } from '../../../cqrs/base/HandlerDecoratorBase';
import { ErrorResult } from '../results/error.result';
import { HttpResult } from '../results/http-result';
import { Ok } from '../results/ok.result';

export class AxiosPromiseUnwrapDecorator<Tin, TOut> extends HandlerDecoratorBase<
  Tin,
  Promise<HttpResult<TOut>>,
  AxiosPromise<TOut>
> {
  async handle(input: Tin): Promise<HttpResult<TOut>> {
    const response = await this.decorated.handle(input);
    if (response.status === HttpStatusCode.Ok) {
      return new Ok(response.data);
    }
    return new ErrorResult<TOut>(response.statusText);
  }
}
