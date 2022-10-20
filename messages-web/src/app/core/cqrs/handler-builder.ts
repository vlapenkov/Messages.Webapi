import { CommandBase } from './base/command.base';
import { HandlerBase } from './base/handler.base';
import { HandlerDecoratorBase } from './base/handler-decorator.base';
import { PipeHandler } from './pipe.handler';
import { QueryBase } from './base/query.base';
import { HandlerInput } from './base/@types/HandlerInput';
import { HandlerOutput } from './base/@types/HandlerOutput';

export class HandlerBuilder<TInput, TOutput, THandler extends HandlerBase<TInput, TOutput>> {
  protected constructor(private handler: THandler) {}

  pipe<TOutputNext, THandlerNext extends HandlerBase<TOutput, TOutputNext>>(next: THandlerNext) {
    return new PipeHandler(this.handler, next);
  }

  wrap<THandlerDecorator extends HandlerDecoratorBase<THandler>>(
    fn: (arg: THandler) => THandlerDecorator,
  ): THandlerDecorator {
    return fn(this.handler);
  }

  static query<TQuery extends QueryBase>(query: TQuery) {
    return new HandlerBuilder<HandlerInput<TQuery>, HandlerOutput<TQuery>, TQuery>(query);
  }

  static command<TCommand extends CommandBase>(handler: TCommand) {
    return new HandlerBuilder<HandlerInput<TCommand>, HandlerOutput<TCommand>, TCommand>(handler);
  }

  getHandler() {
    return this.handler;
  }

  buildFunction(): (arg: TInput) => TOutput {
    return (arg: TInput) => this.handler.handle(arg);
  }
}

export type BuilderOf<T extends HandlerBase> = HandlerBuilder<HandlerInput<T>, HandlerOutput<T>, T>;
