import { HandlerBase } from './base/handler.base';

export class PipeHandler<
  TInput,
  TMiddle,
  TOutput,
  TFirst extends HandlerBase<TMiddle, TInput>,
  TSecond extends HandlerBase<TOutput, TMiddle>,
> extends HandlerBase<TOutput, TInput> {
  constructor(private first: TFirst, private second: TSecond) {
    super();
  }

  handle(input: TInput): TOutput {
    return this.second.handle(this.first.handle(input));
  }
}
