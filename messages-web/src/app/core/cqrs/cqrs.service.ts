/* eslint-disable no-use-before-define */
import { HandlerFunction } from './base/@types/HandlerOutput';
import { QueryBase } from './base/query.base';
import { BuilderOf, HandlerBuilder } from './handler-builder';

export function query<TQueryInitial extends QueryBase, TQueryResult extends QueryBase>(
  handler: TQueryInitial,
  fn: (builder: BuilderOf<TQueryInitial>) => BuilderOf<TQueryResult>,
): HandlerFunction<TQueryResult>;
export function query<TQueryInitial extends QueryBase>(
  handler: TQueryInitial,
): HandlerFunction<TQueryInitial>;
export function query<TQueryInitial extends QueryBase, TQueryResult extends QueryBase>(
  handler: TQueryInitial,
  fn?: (builder: BuilderOf<TQueryInitial>) => BuilderOf<TQueryResult>,
): HandlerFunction<TQueryInitial> | HandlerFunction<TQueryResult> {
  const biulder = HandlerBuilder.query(handler);
  return (fn ? fn(biulder) : biulder).buildFunction();
}
