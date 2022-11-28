import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { description } from '@/app/core/models/decorators/description.decorator';
import { render } from '@/app/core/models/decorators/render.decorator';
import { IOrderModel } from './IOrderModel';

export class OrderModel extends ModelBase<IOrderModel> {
  id = -1;

  @description('Создал')
  createdBy = '';

  @description('Внёс последние изменения')
  lastModifiedBy = '';

  @description('Дата создания')
  @render((m: OrderModel) => (m.created ? m.created.toLocaleDateString() : 'неизвестна'))
  created: Date | null = null;

  @description('Дата последних изменений')
  @render((m: OrderModel) => (m.created ? m.created.toLocaleDateString() : 'неизвестна'))
  lastModified: Date | null = null;

  @description('Организация')
  organisationName = '';

  @description('Имя пользователя')
  userName = '';

  @description('Комментарии')
  comments = '';

  @description('Сумма')
  sum = -1;

  quantity = -1;

  fromResponse(model: IOrderModel): boolean {
    try {
      this.id = model.id;
      this.createdBy = model.createdBy;
      this.lastModifiedBy = model.lastModifiedBy;
      this.created = new Date(model.created);
      this.lastModified = new Date(model.lastModified);
      this.organisationName = model.organisationName;
      this.userName = model.userName;
      this.comments = model.comments;
      this.sum = model.sum;
      this.quantity = model.quantity;
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IOrderModel {
    return {
      created: this.created?.toUTCString() ?? '',
      createdBy: this.createdBy,
      lastModified: this.lastModified?.toUTCString() ?? '',
      lastModifiedBy: this.lastModifiedBy,
      userName: this.userName,
      comments: this.comments,
      sum: this.sum,
      organisationName: this.organisationName,
      id: this.id,
      quantity: this.quantity,
      [modelMarker]: this[modelMarker],
    };
  }

  equals(mb: OrderModel): boolean {
    return this.key === mb.key;
  }

  get key(): string {
    return JSON.stringify(this.toRequest());
  }

  clone(): ModelBase<IModel> {
    const nm = new OrderModel();
    Object.assign(nm, this);
    return nm;
  }
}
