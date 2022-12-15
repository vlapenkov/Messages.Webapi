import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IOrderModel } from './IOrderModel';

export class OrderModel extends ModelBase<IOrderModel> {
  id = -1;

  createdBy = '';

  lastModifiedBy = '';

  created: Date | null = null;

  lastModified: Date | null = null;

  organisationName = '';

  userName = '';

  comments = '';

  sum = -1;

  quantity = -1;

  statusText = '';

  producerName = '';

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
      this.statusText = model.statusText;
      this.producerName = model.producerName;
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
      statusText: this.statusText,
      producerName: this.producerName,
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

