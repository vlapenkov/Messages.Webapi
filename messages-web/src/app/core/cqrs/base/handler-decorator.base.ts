/* eslint-disable @typescript-eslint/no-explicit-any */
import { HandlerInput } from './@types/HandlerInput';
import { HandlerOutput } from './@types/HandlerOutput';
import { HandlerBase } from './handler.base';

export abstract class HandlerDecoratorBase<
  THandler extends HandlerBase,
  TInput = HandlerInput<THandler>,
  TOutput = HandlerOutput<THandler>,
> extends HandlerBase<TOutput, TInput> {
  constructor(protected decorated: THandler) {
    super();
  }
}
