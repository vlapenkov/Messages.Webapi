import { HandlerBase } from './base/handler.base';

export class PipeHandler<
  TInput,
  TMiddle,
  TOutput,
  TFirst extends HandlerBase<TInput, TMiddle>,
  TSecond extends HandlerBase<TMiddle, TOutput>,
> extends HandlerBase<TInput, TOutput> {
  constructor(private first: TFirst, private second: TSecond) {
    super();
  }

  handle(input: TInput): TOutput {
    return this.second.handle(this.first.handle(input));
  }
}
