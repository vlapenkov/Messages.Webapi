/* eslint-disable @typescript-eslint/no-explicit-any */
import { HandlerInput } from './@types/HandlerInput';
import { HandlerOutput } from './@types/HandlerOutput';
import { HandlerBase } from './handler.base';

export abstract class HandlerDecoratorBase<
  THandler extends HandlerBase,
  TInput = HandlerInput<THandler>,
  TOutput = HandlerOutput<THandler>,
> extends HandlerBase<TInput, TOutput> {
  constructor(protected decorated: THandler) {
    super();
  }
}
