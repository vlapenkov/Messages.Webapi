import { ICommand } from './base/@types/ICommand';
import { IQuery } from './base/@types/IQuery';
import { CommandHandlerBase } from './base/CommandHandlerBase';
import { HandlerBase } from './base/HandlerBase';
import { HandlerDecoratorBase } from './base/HandlerDecoratorBase';
import { HandlerPair } from './base/HandlerPair';
import { QueryHandlerBase } from './base/QueryHandlerBase';

export class HandlerBuilder<TInput, TOutput> {
  protected constructor(private handler: HandlerBase<TInput, TOutput>) {}

  pipe<TOutputNext>(next: HandlerBase<TOutput, TOutputNext>): HandlerBase<TInput, TOutputNext> {
    return new HandlerPair(this.handler, next);
  }

  decorate<TInputNew = TInput, TOutputNew = TOutput>(
    fn: (
      arg: HandlerBase<TInput, TOutput>,
    ) => HandlerDecoratorBase<TInputNew, TOutputNew, TInput, TOutput>,
  ): HandlerDecoratorBase<TInputNew, TOutputNew, TInput, TOutput> {
    return fn(this.handler);
  }

  static query<TOut, TIn extends IQuery<TOut>>(handler: QueryHandlerBase<TOut, TIn>) {
    return new HandlerBuilder(handler);
  }

  static command<TOut, TIn extends ICommand<TOut>>(handler: CommandHandlerBase<TOut, TIn>) {
    return new HandlerBuilder(handler);
  }

  getHandler() {
    return this.handler;
  }

  buildFunction(): (arg: TInput) => TOutput {
    return (arg: TInput) => this.handler.handle(arg);
  }
}
