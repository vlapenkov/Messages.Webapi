import { createHandler, Handler } from './handler';
import { HandlerDecorator, HandlerWrapper } from './handler-wrapper';

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

  wrapMany(...decorators: HandlerDecorator<Handler<TOut, Tin>>[]) {
    let lab: HandlerLab<TOut, Tin> = new HandlerLab(this.handler);
    decorators.forEach((d) => {
      lab = lab.wrap(d);
    });
    return lab;
  }
}

export function extend<TOut, Tin = undefined>(handler: Handler<TOut, Tin>) {
  return new HandlerLab(handler);
}
