import { ICommand } from './base/@types/ICommand';
import { CommandHandlerBase } from './base/CommandHandlerBase';

export class CommandHandlerGeneric<
  TOutput,
  TInput extends ICommand<TOutput>,
> extends CommandHandlerBase<TOutput, TInput> {
  constructor(private innerHandler: (arg: TInput) => TOutput) {
    super();
  }

  handle(input: TInput): TOutput {
    return this.innerHandler(input);
  }
}
