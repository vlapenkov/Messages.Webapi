import { HandlerBase } from './HandlerBase';

export abstract class HandlerDecoratorBase<
  TInput,
  TOutput,
  TOutInner = TOutput,
  TInInner = TInput,
> extends HandlerBase<TInput, TOutput> {
  constructor(protected decorated: HandlerBase<TInInner, TOutInner>) {
    super();
  }
}
