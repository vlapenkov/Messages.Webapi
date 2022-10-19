import { HttpResult } from './http-result';
import { HttpStatus } from './http-status';

export class Ok<T> extends HttpResult<T> {
  status: HttpStatus = HttpStatus.Success;

  constructor(public data: T) {
    super();
  }
}
