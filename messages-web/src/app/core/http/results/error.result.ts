import { HttpResult } from './http-result';
import { HttpStatus } from './http-status';

export class ErrorResult<T> extends HttpResult<T> {
  status: HttpStatus = HttpStatus.Error;

  constructor(public message: string) {
    super();
  }
}
