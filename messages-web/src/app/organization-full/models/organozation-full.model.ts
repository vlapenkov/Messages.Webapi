import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { description } from '@/app/core/models/decorators/description.decorator';
import { hidden } from '@/app/core/models/decorators/hidden.decorator';
import { title } from '@/app/core/models/decorators/tittle.decorator';
import { IOrganizationFullMiodel } from '../@types/IOrganizationFullModel';

export class OrganizationFullModel
  extends ModelBase<IOrganizationFullMiodel>
  implements IOrganizationFullMiodel
{
  @hidden()
  id = 0;

  @title
  @description('Название')
  name = '';

  @description('Полное наименование')
  fullName = '';

  @description('ОГРН')
  ogrn = '';

  @description('ИНН')
  inn = '';

  @description('КПП')
  kpp = '';

  @description('Регион')
  region = '';

  @description('Город')
  city = '';

  @description('Адрес')
  address = '';

  @description('ОКВЕД')
  okved = '';

  @description('ОКВЕД2')
  okved2 = '';

  @description('Сайт')
  site = '';

  statusText = '';

  lastModified = '';

  lastModifiedBy = '';

  created = '';

  createdBy = '';

  fromResponse(model: IOrganizationFullMiodel): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IOrganizationFullMiodel {
    return this;
  }

  equals(mb: OrganizationFullModel): boolean {
    return (
      this.id === mb.id &&
      this.name === mb.name &&
      this.fullName === mb.fullName &&
      this.ogrn === mb.ogrn &&
      this.inn === mb.inn &&
      this.kpp === mb.kpp &&
      this.region === mb.region &&
      this.city === mb.city &&
      this.address === mb.address &&
      this.site === mb.site &&
      this.okved === mb.okved &&
      this.okved2 === mb.okved2
    );
  }

  get key(): string | number | symbol {
    return this.id;
  }

  clone(): ModelBase<IModel> {
    const cloned = new OrganizationFullModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
