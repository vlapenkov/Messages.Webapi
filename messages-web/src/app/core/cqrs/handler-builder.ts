import { CommandBase } from './base/command.base';
import { HandlerBase } from './base/handler.base';
import { HandlerDecoratorBase } from './base/handler-decorator.base';
import { PipeHandler } from './pipe.handler';
import { QueryBase } from './base/query.base';
import { HandlerInput } from './base/@types/HandlerInput';
import { HandlerOutput } from './base/@types/HandlerOutput';

export class HandlerBuilder<THandler extends HandlerBase> {
  protected constructor(private handler: THandler) {}

  pipe<THandlerNext extends HandlerBase>(next: THandlerNext) {
    return new HandlerBuilder(new PipeHandler(this.handler, next));
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  wrap<THandlerDecorator extends HandlerDecoratorBase<THandler, any, any>>(
    fn: (arg: THandler) => THandlerDecorator,
  ) {
    return new HandlerBuilder(fn(this.handler));
  }

  static query<TQuery extends QueryBase>(query: TQuery) {
    return new HandlerBuilder<TQuery>(query);
  }

  static command<TCommand extends CommandBase>(handler: TCommand) {
    return new HandlerBuilder<TCommand>(handler);
  }

  getHandler() {
    return this.handler;
  }

  buildFunction(): (arg: HandlerInput<THandler>) => HandlerOutput<THandler> {
    return (arg: HandlerInput<THandler>) => this.handler.handle(arg);
  }
}
