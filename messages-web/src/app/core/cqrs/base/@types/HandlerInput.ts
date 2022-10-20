import { HandlerBase } from '../handler.base';

export type HandlerInput<THandler extends HandlerBase> = Parameters<THandler['handle']>[0];
