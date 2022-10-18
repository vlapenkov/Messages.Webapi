import { IQuery } from './base/@types/IQuery';
import { QueryHandlerBase } from './base/QueryHandlerBase';

export class QueryHandlerGeneric<TOutput, TInput extends IQuery<TOutput>> extends QueryHandlerBase<
  TOutput,
  TInput
> {
  constructor(private innerHandler: (arg: TInput) => TOutput) {
    super();
  }

  handle(input: TInput): TOutput {
    return this.innerHandler(input);
  }
}
