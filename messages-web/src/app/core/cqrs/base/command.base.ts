/* eslint-disable @typescript-eslint/no-explicit-any */
import { ICommand } from './@types/ICommand';
import { HandlerBase } from './handler.base';

export abstract class CommandBase<
  TOutput = any,
  TInput extends ICommand<TOutput> | undefined = undefined,
> extends HandlerBase<TOutput, TInput> {}
