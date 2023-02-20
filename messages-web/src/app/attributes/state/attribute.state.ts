import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { AttributeModel } from '../models/AttributeModel';

export class AttributeState extends StateBase {
  items: AttributeModel[] | null = null;

  status = new DataStatus();

  selectedItem: NotValidData<AttributeModel> | null = null;
}
