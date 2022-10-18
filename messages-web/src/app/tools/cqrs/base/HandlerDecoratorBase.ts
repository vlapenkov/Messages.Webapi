import { HandlerBase } from './HandlerBase';

export abstract class HandlerDecorator<TInput, TOutput> extends HandlerBase<TInput, TOutput> {
  constructor(protected decorated: HandlerBase<TInput, TOutput>) {
    super();
  }
}
