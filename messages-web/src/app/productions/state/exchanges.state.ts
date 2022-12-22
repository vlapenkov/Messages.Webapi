import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { ExcahngeModel } from '../models/exchange.model';

export class ExchangesState extends StateBase {
  list: ExcahngeModel[] = [];

  status = new DataStatus();
}
