import { Handler } from './Handler';

export abstract class HandlerDecorator<TInput, TOutput> extends Handler<TInput, TOutput> {
  constructor(protected decorated: Handler<TInput, TOutput>) {
    super();
  }
}
