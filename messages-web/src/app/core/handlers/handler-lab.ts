import { createHandler, Handler } from './handler';
import { HandlerWrapper } from './handler-wrapper';

class HandlerLab<TOut, Tin = undefined> {
  constructor(private handler: Handler<TOut, Tin>) {}

  pipe<TOutNext>(next: Handler<TOutNext, TOut>): HandlerLab<TOutNext, Tin> {
    const resultHandler = createHandler(() => (arg: Tin) => next(this.handler(arg)));
    return new HandlerLab(resultHandler);
  }

  done(): Handler<TOut, Tin> {
    return this.handler;
  }

  wrap<TOutNew, TinNew = undefined>(
    wrapper: HandlerWrapper<Handler<TOut, Tin>, Handler<TOutNew, TinNew>>,
  ): HandlerLab<TOutNew, TinNew> {
    const resultHandler = wrapper(this.handler);
    return new HandlerLab(resultHandler);
  }
}

export function extend<TOut, Tin = undefined>(handler: Handler<TOut, Tin>) {
  return new HandlerLab(handler);
}
