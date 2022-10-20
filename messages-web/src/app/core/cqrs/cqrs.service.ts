/* eslint-disable no-use-before-define */
import { HandlerFunction } from './base/@types/HandlerOutput';
import { CommandBase } from './base/command.base';
import { QueryBase } from './base/query.base';
import { HandlerBuilder } from './handler-builder';

export function command<TCommandInitial extends CommandBase>(
  handler: TCommandInitial,
): HandlerFunction<TCommandInitial>;
export function command<TCommandInitial extends CommandBase, TCommandResult extends CommandBase>(
  handler: TCommandInitial,
  fn: (builder: HandlerBuilder<TCommandInitial>) => HandlerBuilder<TCommandResult>,
): HandlerFunction<TCommandResult>;
export function command<TCommandInitial extends CommandBase, TCommandResult extends CommandBase>(
  handler: TCommandInitial,
  fn?: (builder: HandlerBuilder<TCommandInitial>) => HandlerBuilder<TCommandResult>,
): HandlerFunction<TCommandInitial> | HandlerFunction<TCommandResult> {
  const biulder = HandlerBuilder.command(handler);
  return (fn ? fn(biulder) : biulder).buildFunction();
}

export function query<
  TQueryInitial extends QueryBase = QueryBase,
  TQueryResult extends QueryBase = QueryBase,
>(
  handler: TQueryInitial,
  fn: (builder: HandlerBuilder<TQueryInitial>) => HandlerBuilder<TQueryResult>,
): HandlerFunction<TQueryResult>;
export function query<TQueryInitial extends QueryBase>(
  handler: TQueryInitial,
): HandlerFunction<TQueryInitial>;
export function query<TQueryInitial extends QueryBase, TQueryResult extends QueryBase>(
  handler: TQueryInitial,
  fn?: (builder: HandlerBuilder<TQueryInitial>) => HandlerBuilder<TQueryResult>,
): HandlerFunction<TQueryInitial> | HandlerFunction<TQueryResult> {
  const biulder = HandlerBuilder.query(handler);
  return (fn ? fn(biulder) : biulder).buildFunction();
}
