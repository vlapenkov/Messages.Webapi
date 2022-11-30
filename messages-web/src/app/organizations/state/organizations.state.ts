import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { collection } from '@/app/core/services/harlem/state/decorators/collection.decorator';
import { dataStatus } from '@/app/core/services/harlem/state/decorators/data-status.decorator';
import { selected } from '@/app/core/services/harlem/state/decorators/selected-item.decorator';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { OrganizationModel } from '../model/organization.model';

export class OrganizationsState extends StateBase {
  @collection
  collection: OrganizationModel[] | null = null;

  @dataStatus
  status = new DataStatus();

  @selected({
    create: false,
    delete: false,
    update: false,
  })
  dontUse: NotValidData<OrganizationModel> | null = null;
}
