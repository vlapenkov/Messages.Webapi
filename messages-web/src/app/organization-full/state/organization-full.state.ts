import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { dataStatus } from '@/app/core/services/harlem/state/decorators/data-status.decorator';
import { ignoreMounted } from '@/app/core/services/harlem/state/decorators/ignore-mounted.decorator';
import { item } from '@/app/core/services/harlem/state/decorators/item.decorator';
import { selected } from '@/app/core/services/harlem/state/decorators/selected-item.decorator';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { OrganizationFullModel } from '../models/organozation-full.model';

export class OrganizationFullState extends StateBase {
  @ignoreMounted
  @item
  item = new OrganizationFullModel();

  @dataStatus
  status = new DataStatus('loaded');

  @selected()
  itemSelected: OrganizationFullModel | null = null;
}
