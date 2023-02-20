import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { ShoppingCartModel } from '../models/shopping-cart.model';

export class ShoppingCartState extends StateBase {
  cartItems: ShoppingCartModel[] | null = null;

  status = new DataStatus();

  dontUse: NotValidData<ShoppingCartModel> | null = null;
}
