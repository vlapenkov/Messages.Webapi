/* eslint-disable @typescript-eslint/no-explicit-any */

import { HandlerBase } from './handler.base';
import { IQuery } from './@types/IQuery';

export abstract class QueryBase<
  TOutput = any,
  TInput extends IQuery<TOutput> | undefined = undefined,
> extends HandlerBase<TOutput, TInput> {}
