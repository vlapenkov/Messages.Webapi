import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';

export interface IProductShortModel extends IModel {
  price: number;
  description: string;
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
}

export class ProductShortModel extends ModelBase<IProductShortModel> {
  price: number | null = null;

  description = '';

  createdBy: string | null = null;

  lastModifiedBy: string | null = null;

  created: Date | null = null;

  lastModified: Date | null = null;

  get key(): string {
    return JSON.stringify({
      price: this.price,
      description: this.description,
    });
  }

  fromResponse(model: IProductShortModel): boolean {
    try {
      this.description = model.description;
      this.price = model.price;
      this.createdBy = model.createdBy;
      this.lastModifiedBy = model.lastModifiedBy;
      this.created = new Date(model.created);
      this.lastModified = new Date(model.lastModified);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IProductShortModel {
    return {
      created: this.created?.toUTCString() ?? '',
      lastModified: this.lastModified?.toUTCString() ?? '',
      createdBy: this.createdBy ?? '',
      lastModifiedBy: this.lastModifiedBy ?? '',
      description: this.description,
      price: this.price ?? 0,
      [modelMarker]: this[modelMarker],
    };
  }

  equals(mb: ProductShortModel): boolean {
    return mb.price === this.price && mb.description === this.description;
  }

  clone(): ProductShortModel {
    const cloned = new ProductShortModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
