import { ModelBase } from '@/app/core/models/base/model-base';
import { IShoppingCartModel } from '../@types/IShoppingCartModel';

export class ShoppingCartModel extends ModelBase<IShoppingCartModel> implements IShoppingCartModel {
  productId = 0;

  productName = '';

  documentId = '';

  price = 0;

  quantity = 0;

  sum = 0;

  fromResponse(model: IShoppingCartModel): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IShoppingCartModel {
    return this;
  }

  equals(other: ShoppingCartModel): boolean {
    return (
      this.documentId === other.documentId &&
      this.productName === other.productName &&
      this.price === other.price &&
      this.quantity === other.quantity &&
      this.sum === other.sum
    );
  }

  get key(): string | number | symbol {
    return this.productId + this.documentId;
  }

  clone(): ShoppingCartModel {
    const cloned = new ShoppingCartModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
