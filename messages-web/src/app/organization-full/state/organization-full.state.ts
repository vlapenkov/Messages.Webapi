import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { OrganizationFullModel } from '../models/organozation-full.model';

export class OrganizationFullState extends StateBase {
  item: OrganizationFullModel | null = null;

  status = new DataStatus();

  itemSelected: NotValidData<OrganizationFullModel> | null = null;
}
