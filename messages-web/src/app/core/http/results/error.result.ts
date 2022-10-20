import { HttpResult } from './base/http-result';
import { HttpStatus } from './base/http-status';

export class ErrorResult<T> extends HttpResult<T> {
  status: HttpStatus = HttpStatus.Error;

  constructor(public message: string) {
    super();
  }
}
