import { ModelBase } from '@/app/core/models/base/model-base';
import { description } from '@/app/core/models/decorators/description.decorator';
import { hidden } from '@/app/core/models/decorators/hidden.decorator';
import { IAttribute } from '../@types/IAttribute';

export class AttributeModel extends ModelBase<IAttribute> implements IAttribute {
  @hidden()
  id = 0;

  @description('Название')
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
