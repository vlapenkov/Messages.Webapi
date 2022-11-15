import { ModelBase } from '@/app/core/models/base/model-base';
import { IProductFullModel } from '../@types/IProductFullModel';

export class ProductFullModel extends ModelBase<IProductFullModel> implements IProductFullModel {
  id = 0;

  name = '';

  fullName = '';

  catalogSectionId = 0;

  description = '';

  codeTnVed = '';

  price = 0;

  measuringUnit = '';

  country = '';

  currency = '';

  status = 0;

  attributeValues: { baseProductId: number; attributeId: number; value: string }[] = [];

  documents: { fileName: string; data: string; fileId: string }[] = [];

  fromResponse(model: IProductFullModel): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IProductFullModel {
    return this;
  }

  equals(mb: ProductFullModel): boolean {
    return (
      this.id === mb.id &&
      this.name === mb.name &&
      this.fullName === mb.fullName &&
      this.catalogSectionId === mb.catalogSectionId &&
      this.description === mb.description &&
      this.codeTnVed === mb.codeTnVed &&
      this.price === mb.price &&
      this.measuringUnit === mb.measuringUnit &&
      this.country === mb.country &&
      this.currency === mb.currency &&
      this.status === mb.status &&
      this.attributeValues.length === mb.attributeValues.length &&
      this.attributeValues.every((item, index) => {
        const { attributeId, baseProductId, value } = mb.attributeValues[index];
        return (
          item.attributeId === attributeId &&
          item.baseProductId === baseProductId &&
          item.value === value
        );
      }) &&
      this.documents.length === mb.documents.length &&
      this.documents.every((item, index) => {
        const { data, fileId, fileName } = mb.documents[index];
        return item.data === data && item.fileId === fileId && item.fileName === fileName;
      })
    );
  }

  get key(): string | number | symbol {
    return this.id;
  }

  clone(): ProductFullModel {
    const cloned = new ProductFullModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
