import { HandlerBase } from './HandlerBase';

export abstract class HandlerDecoratorBase<TInput, TOutput> extends HandlerBase<TInput, TOutput> {
  constructor(protected decorated: HandlerBase<TInput, TOutput>) {
    super();
  }
}
