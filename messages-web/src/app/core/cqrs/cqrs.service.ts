import { ICommand } from './base/@types/ICommand';
import { IQuery } from './base/@types/IQuery';
import { CommandBase } from './base/command.base';
import { QueryBase } from './base/query.base';
import { HandlerBuilder } from './handler-builder';

export function command<TOut, TIn extends ICommand<TOut>, TInResult, TOutResult>(
  handler: CommandBase<TOut, TIn>,
  fn: (builder: HandlerBuilder<TIn, TOut>) => HandlerBuilder<TInResult, TOutResult>,
): (arg: TInResult) => TOutResult {
  const biulder = HandlerBuilder.command<TOut, TIn>(handler);
  return fn(biulder).buildFunction();
}

export function query<TOut, TIn extends IQuery<TOut>, TOutResult, TInResult>(
  handler: QueryBase<TOut, TIn>,
  fn: (builder: HandlerBuilder<TIn, TOut>) => HandlerBuilder<TInResult, TOutResult>,
): (arg: TInResult) => TOutResult {
  const biulder = HandlerBuilder.query<TOut, TIn>(handler);
  return fn(biulder).buildFunction();
}
