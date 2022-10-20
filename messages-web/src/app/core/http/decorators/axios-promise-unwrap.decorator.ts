import { HttpStatusCode } from 'axios';
import { HandlerDecoratorBase } from '../../cqrs/base/handler-decorator.base';
import { ErrorResult } from '../results/error.result';
import { HttpResult } from '../results/base/http-result';
import { Ok } from '../results/ok.result';
import { AxiosHandler } from '../handlers/axios/axios.handler';
import { HandlerInput } from '../../cqrs/base/@types/HandlerInput';
import { HandlerOutput } from '../../cqrs/base/@types/HandlerOutput';

export class AxiosPromiseUnwrapDecorator<
  THandler extends AxiosHandler,
  TIn extends HandlerInput<AxiosHandler> = HandlerInput<THandler>,
  TOutData extends Awaited<HandlerOutput<AxiosHandler>> = Awaited<HandlerOutput<THandler>>,
> extends HandlerDecoratorBase<THandler, TIn, Promise<HttpResult<TOutData>>> {
  async handle(input: TIn): Promise<HttpResult<TOutData>> {
    const response = await this.decorated.handle(input);
    if (response.status === HttpStatusCode.Ok) {
      return new Ok(response.data);
    }
    return new ErrorResult<TOutData>(response.statusText);
  }
}
