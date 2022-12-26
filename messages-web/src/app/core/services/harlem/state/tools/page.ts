import { DataStatus } from '../../tools/data-status';

export class Page<TRequest, TData> {
  data: TData | undefined;

  status: DataStatus = new DataStatus('loading');

  constructor(public request: TRequest) {}
}
