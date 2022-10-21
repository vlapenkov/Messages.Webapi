import { Handler } from '@/app/core/handlers/handler';

export type GetUrlHandler<T = undefined> = Handler<string, T>;
