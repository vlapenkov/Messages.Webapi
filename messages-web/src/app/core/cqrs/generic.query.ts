import { IQuery } from './base/@types/IQuery';
import { QueryBase } from './base/query.base';

export class GenericQuery<TOutput, TInput extends IQuery<TOutput>> extends QueryBase<
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
