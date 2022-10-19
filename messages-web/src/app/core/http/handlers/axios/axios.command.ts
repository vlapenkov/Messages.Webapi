import { ICommand } from '@/app/core/cqrs/base/@types/ICommand';
import { AxiosHandler } from './axios.handler';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export class AxiosCommand<TOut, Tin extends ICommand<TOut>> extends AxiosHandler<Tin, TOut> {}
