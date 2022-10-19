import { IQuery } from '@/app/core/services/cqrs/base/@types/IQuery';
import { AxiosHandler } from './axios.handler';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export class AxiosQuery<TOut, Tin extends IQuery<TOut>> extends AxiosHandler<Tin, TOut> {}
