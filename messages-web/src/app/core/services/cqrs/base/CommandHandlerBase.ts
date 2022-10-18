import { ICommand } from './@types/ICommand';
import { HandlerBase } from './HandlerBase';

export abstract class CommandHandlerBase<
  TOutput,
  TInput extends ICommand<TOutput>,
> extends HandlerBase<TInput, TOutput> {}
