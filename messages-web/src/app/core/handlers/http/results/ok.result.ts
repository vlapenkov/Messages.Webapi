import { HttpResult } from './base/http-result';
import { HttpStatus } from './base/http-status';

export class Ok<T> extends HttpResult<T> {
  status: HttpStatus = HttpStatus.Success;

  constructor(public data: T) {
    super();
  }
}
