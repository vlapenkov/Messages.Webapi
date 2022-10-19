import { HttpStatus } from './http-status';

export abstract class HttpResult<T> {
  data: T | undefined;

  abstract status: HttpStatus;

  message: string | undefined;
}
