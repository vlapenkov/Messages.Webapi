import { HandlerBase } from '../handler.base';

export type HandlerOutput<THandler extends HandlerBase> = ReturnType<THandler['handle']>;

export type HandlerFunction<THandler extends HandlerBase> = THandler['handle'];
