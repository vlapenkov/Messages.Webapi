import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';

export type ProductionType = 'Product' | 'ServiceProduct' | 'WorkProduct';

export interface IProductionModel extends IModel {
  id: number;
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
  name: string;
  description: string;
  statusText: string;
  availableStatusText: string;
  price: number | null;
  productionType: ProductionType;
  rating: number | null;
  documentId: string;
  organization: {
    id: number;
    name: string;
    region: string;
  };
}

export class ProductionModel extends ModelBase<IProductionModel> implements IProductionModel {
  id = -1;

  createdBy = '';

  lastModifiedBy = '';

  created = '';

  lastModified = '';

  name = '';

  description = '';

  statusText = '';

  availableStatusText = '';

  price: number | null = null;

  rating: number | null = null;

  productionType: ProductionType = 'Product';

  organization: { id: number; name: string; region: string } = {
    id: -1,
    name: '',
    region: '',
  };

  documentId = '';

  get key(): number {
    return this.id;
  }

  fromResponse(model: IProductionModel): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IProductionModel {
    return {
      ...this,
      [modelMarker]: null as never,
    };
  }

  equals(mb: ProductionModel): boolean {
    return mb.price === this.price && mb.description === this.description;
  }

  clone(): ProductionModel {
    const cloned = new ProductionModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
