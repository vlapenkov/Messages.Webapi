import { CommandBase } from '../command.base';
import { HandlerBase } from '../handler.base';
import { QueryBase } from '../query.base';
import { HandlerBuilder } from '../../handler-builder';

export type HandlerTransformer<
  THandler extends HandlerBase = HandlerBase,
  TResultHandler extends HandlerBase = HandlerBase,
> = (arg: HandlerBuilder<THandler>) => HandlerBuilder<TResultHandler>;

export type CommandTransformer<TCommand extends CommandBase, TResultCommand extends CommandBase> = (
  arg: HandlerBuilder<TCommand>,
) => HandlerBuilder<TResultCommand>;

export type QueryTransformer<TQuery extends QueryBase, TResultQuery extends QueryBase> = (
  arg: HandlerBuilder<TQuery>,
) => HandlerBuilder<TResultQuery>;
