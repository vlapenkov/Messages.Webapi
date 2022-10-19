import { ICommand } from './base/@types/ICommand';
import { IQuery } from './base/@types/IQuery';
import { CommandHandlerBase } from './base/CommandHandlerBase';
import { QueryHandlerBase } from './base/QueryHandlerBase';
import { HandlerBuilder } from './HandlerBuilder';

export function command<TOut, TIn extends ICommand<TOut>, TInResult, TOutResult>(
  handler: CommandHandlerBase<TOut, TIn>,
  fn: (builder: HandlerBuilder<TIn, TOut>) => HandlerBuilder<TInResult, TOutResult>,
): (arg: TInResult) => TOutResult {
  const biulder = HandlerBuilder.command<TOut, TIn>(handler);
  return fn(biulder).buildFunction();
}

export function query<TOut, TIn extends IQuery<TOut>, TInResult, TOutResult>(
  handler: QueryHandlerBase<TOut, TIn>,
  fn: (builder: HandlerBuilder<TIn, TOut>) => HandlerBuilder<TInResult, TOutResult>,
): (arg: TInResult) => TOutResult {
  const biulder = HandlerBuilder.query<TOut, TIn>(handler);
  return fn(biulder).buildFunction();
}
