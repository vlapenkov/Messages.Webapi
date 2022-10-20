import { ICommand } from './base/@types/ICommand';
import { IQuery } from './base/@types/IQuery';
import { CommandBase } from './base/command.base';
import { HandlerBase } from './base/handler.base';
import { HandlerDecoratorBase } from './base/handler-decorator.base';
import { PipeHandler } from './pipe.handler';
import { QueryBase } from './base/query.base';

export class HandlerBuilder<TInput, TOutput> {
  protected constructor(private handler: HandlerBase<TInput, TOutput>) {}

  pipe<TOutputNext>(next: HandlerBase<TOutput, TOutputNext>): HandlerBase<TInput, TOutputNext> {
    return new PipeHandler(this.handler, next);
  }

  decorate<TInputNew = TInput, TOutputNew = TOutput>(
    fn: (
      arg: HandlerBase<TInput, TOutput>,
    ) => HandlerDecoratorBase<TInputNew, TOutputNew, TInput, TOutput>,
  ): HandlerDecoratorBase<TInputNew, TOutputNew, TInput, TOutput> {
    return fn(this.handler);
  }

  static query<TOut, TIn extends IQuery<TOut>>(handler: QueryBase<TOut, TIn>) {
    return new HandlerBuilder(handler);
  }

  static command<TOut, TIn extends ICommand<TOut>>(handler: CommandBase<TOut, TIn>) {
    return new HandlerBuilder(handler);
  }

  getHandler() {
    return this.handler;
  }

  buildFunction(): (arg: TInput) => TOutput {
    return (arg: TInput) => this.handler.handle(arg);
  }
}
