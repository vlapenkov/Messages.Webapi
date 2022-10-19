import { HandlerBase } from './HandlerBase';
import { IQuery } from './@types/IQuery';

export abstract class QueryHandlerBase<TOutput, TInput extends IQuery<TOutput>> extends HandlerBase<
  TInput,
  TOutput
> {}
