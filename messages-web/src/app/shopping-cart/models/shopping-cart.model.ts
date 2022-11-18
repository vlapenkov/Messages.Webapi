import { ModelBase } from '@/app/core/models/base/model-base';
import { description } from '@/app/core/models/decorators/description.decorator';
import { hidden } from '@/app/core/models/decorators/hidden.decorator';
import { render } from '@/app/core/models/decorators/render.decorator';
import { title } from '@/app/core/models/decorators/tittle.decorator';
import ShoppingCartSelector from '@/vue/containers/shopping-cart-selector.vue';
import { h } from 'vue';
import { IShoppingCartModel } from '../@types/IShoppingCartModel';

export class ShoppingCartModel extends ModelBase<IShoppingCartModel> implements IShoppingCartModel {
  @hidden()
  productId = 0;

  @title
  productName = '';

  @hidden()
  documentId = '';

  @description('Цена')
  price = 0;

  @description('Количество')
  quantity = 0;

  @description('Итоговая цена')
  sum = 0;

  @description('Выбрано')
  @render(() => h(ShoppingCartSelector), 'view')
  selected = false;

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
