import { ICommand } from './@types/ICommand';
import { HandlerBase } from './handler.base';

export abstract class CommandBase<TOutput, TInput extends ICommand<TOutput>> extends HandlerBase<
  TInput,
  TOutput
> {}
