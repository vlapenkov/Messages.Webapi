import { ModelBase } from '@/app/core/models/base/model-base';
import { IAttribute } from '../@types/IAttribute';

export class AttributeModel extends ModelBase<IAttribute> implements IAttribute {
  id = 0;

  name = '';

  fromResponse(model: IAttribute): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IAttribute {
    return this;
  }

  equals(other: AttributeModel): boolean {
    return this.id === other.id && this.name === other.name;
  }

  get key(): string | number | symbol {
    return this.id;
  }

  clone(): AttributeModel {
    const cloned = new AttributeModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
