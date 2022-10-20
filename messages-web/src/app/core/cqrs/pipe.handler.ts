import { HandlerBase } from './base/handler.base';

export class PipeHandler<TInput, TMiddle, TOutput> extends HandlerBase<TInput, TOutput> {
  constructor(
    private first: HandlerBase<TInput, TMiddle>,
    private second: HandlerBase<TMiddle, TOutput>,
  ) {
    super();
  }

  handle(input: TInput): TOutput {
    return this.second.handle(this.first.handle(input));
  }
}
