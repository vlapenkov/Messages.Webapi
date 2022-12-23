import { DataStatus } from '../../tools/data-status';

export class Page<TRequest, TData> {
  data: TData | undefined;

  status = new DataStatus('loading');

  constructor(public request: TRequest) {}
}
