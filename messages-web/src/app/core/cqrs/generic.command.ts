import { ICommand } from './base/@types/ICommand';
import { CommandBase } from './base/command.base';

export class GenericCommand<TOutput, TInput extends ICommand<TOutput>> extends CommandBase<
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
