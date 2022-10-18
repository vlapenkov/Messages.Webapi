import { HandlerBase } from './HandlerBase';

export class HandlerPair<TInput, TMiddle, TOutput> extends HandlerBase<TInput, TOutput> {
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
