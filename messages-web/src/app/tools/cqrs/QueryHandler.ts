import { Handler } from './Handler';
import { IQuery } from './@types/IQuery';

export abstract class QueryHandler<TOutput, TInput extends IQuery<TOutput>> extends Handler<
  TInput,
  TOutput
> {}
