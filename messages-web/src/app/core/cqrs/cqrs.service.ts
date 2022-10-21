/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable no-use-before-define */
import { CommandTransformer, QueryTransformer } from './base/@types/transformers';
import { HandlerFunction } from './base/@types/HandlerOutput';
import { CommandBase } from './base/command.base';
import { QueryBase } from './base/query.base';
import { HandlerBuilder } from './handler-builder';

export function command<TCommandInitial extends CommandBase<any, any>>(
  handler: TCommandInitial,
): HandlerFunction<TCommandInitial>;
export function command<
  TCommandInitial extends CommandBase<any, any>,
  TCommandResult extends CommandBase<any, any>,
>(
  handler: TCommandInitial,
  fn: CommandTransformer<TCommandInitial, TCommandResult>,
): HandlerFunction<TCommandResult>;
export function command<
  TCommandInitial extends CommandBase<any, any>,
  TCommandResult extends CommandBase<any, any>,
>(
  handler: TCommandInitial,
  fn?: CommandTransformer<TCommandInitial, TCommandResult>,
): HandlerFunction<TCommandInitial> | HandlerFunction<TCommandResult> {
  const biulder = HandlerBuilder.command(handler);
  return (fn ? fn(biulder) : biulder).buildFunction();
}

export function query<
  TQueryInitial extends QueryBase<any, any> = QueryBase,
  TQueryResult extends QueryBase<any, any> = QueryBase,
>(
  handler: TQueryInitial,
  fn: QueryTransformer<TQueryInitial, TQueryResult>,
): HandlerFunction<TQueryResult>;
export function query<TQueryInitial extends QueryBase<any, any>>(
  handler: TQueryInitial,
): HandlerFunction<TQueryInitial>;
export function query<
  TQueryInitial extends QueryBase<any, any>,
  TQueryResult extends QueryBase<any, any>,
>(
  handler: TQueryInitial,
  fn?: QueryTransformer<TQueryInitial, TQueryResult>,
): HandlerFunction<TQueryInitial> | HandlerFunction<TQueryResult> {
  const biulder = HandlerBuilder.query(handler);
  return (fn ? fn(biulder) : biulder).buildFunction();
}
