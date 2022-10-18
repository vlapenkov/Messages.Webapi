import { ICommand } from './@types/ICommand';
import { Handler } from './Handler';

export abstract class CommandHandler<TOutput, TInput extends ICommand<TOutput>> extends Handler<
  TInput,
  TOutput
> {}
