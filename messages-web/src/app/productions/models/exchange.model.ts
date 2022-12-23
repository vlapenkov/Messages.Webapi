import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';

export interface IExchangeModel extends IModel {
  id: number;
  createdBy?: string;
  lastModifiedBy?: string;
  created: string;
  lastModified: string;
  productExchangeText?: string;
  productsLoaded: number;
}

export class ExcahngeModel extends ModelBase<IExchangeModel> implements IExchangeModel {
  id = -1;

  createdBy?: string | undefined;

  lastModifiedBy?: string | undefined;

  created = '';

  lastModified = '';

  productExchangeText?: string | undefined;

  productsLoaded = -1;

  fromResponse(model: IExchangeModel): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IExchangeModel {
    return {
      ...this,
      [modelMarker]: null as never,
    };
  }

  equals(mb: ExcahngeModel): boolean {
    return (
      mb.id === this.id &&
      mb.createdBy === this.createdBy &&
      mb.lastModifiedBy === this.lastModifiedBy &&
      mb.created === this.created &&
      mb.lastModified === this.lastModified &&
      mb.productExchangeText === this.productExchangeText &&
      mb.productsLoaded === this.productsLoaded
    );
  }

  get key(): number {
    return this.id;
  }

  clone(): ModelBase<IModel> {
    const cloned = new ExcahngeModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
