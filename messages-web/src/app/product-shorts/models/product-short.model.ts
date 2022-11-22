import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';

export interface IProductShortModel extends IModel {
  id: number;
  documentId: string;
  name: string;
  price: number;
  description: string;
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
  organization: {
    id: number;
    name: string;
    region: string;
  };
}

export class ProductShortModel extends ModelBase<IProductShortModel> {
  id = -1;

  description = '';

  name = '';

  price: number | null = null;

  createdBy: string | null = null;

  lastModifiedBy: string | null = null;

  created: Date | null = null;

  lastModified: Date | null = null;

  organization: {
    id: number;
    name: string;
    region: string;
  } = {
    id: -1,
    name: 'Неизвестная организация',
    region: 'Неизвнстный регион',
  };

  documentId = '';

  get key(): string {
    return JSON.stringify({
      price: this.price,
      description: this.description,
    });
  }

  fromResponse(model: IProductShortModel): boolean {
    try {
      this.id = model.id;
      this.description = model.description;
      this.price = model.price;
      this.createdBy = model.createdBy;
      this.lastModifiedBy = model.lastModifiedBy;
      this.created = new Date(model.created);
      this.lastModified = new Date(model.lastModified);
      this.name = model.name;
      this.organization = model.organization;
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IProductShortModel {
    return {
      id: this.id,
      documentId: this.documentId,
      created: this.created?.toUTCString() ?? '',
      lastModified: this.lastModified?.toUTCString() ?? '',
      createdBy: this.createdBy ?? '',
      lastModifiedBy: this.lastModifiedBy ?? '',
      description: this.description,
      price: this.price ?? 0,
      name: this.name,
      organization: this.organization,
      [modelMarker]: null as never,
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
