import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { ProductFullModel } from '../models/product-full.model';

export class ProductFullState extends StateBase {
  item = new ProductFullModel();

  status = new DataStatus();

  itemSelected: NotValidData<ProductFullModel> | null = null;
}
