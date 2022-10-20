import { HandlerBase } from './handler.base';
import { IQuery } from './@types/IQuery';

export abstract class QueryBase<TOutput, TInput extends IQuery<TOutput>> extends HandlerBase<
  TInput,
  TOutput
> {}
