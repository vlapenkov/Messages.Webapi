import { ModelBase } from '@/app/core/models/base/model-base';
import type { IOrganizationFullModel } from '../@types/IOrganizationFullModel';

export class OrganizationFullModel
  extends ModelBase<IOrganizationFullModel>
  implements IOrganizationFullModel
{
  id = 0;

  name = '';

  fullName = '';

  ogrn = '';

  inn = '';

  kpp = '';

  region = '';

  city = '';

  address = '';

  factAddress = '';

  okved = '';

  okved2 = '';

  site = '';

  phone = '';

  email = '';

  isProducer = false;

  isBuyer = false;

  bankName = '';

  account = '';

  corrAccount = '';

  bik = '';

  latitude = 0;

  longitude = 0;

  statusText = '';

  lastModified = '';

  lastModifiedBy = '';

  created = '';

  createdBy = '';

  documentId = '';

  fromResponse(model: IOrganizationFullModel): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IOrganizationFullModel {
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
      this.okved2 === mb.okved2 &&
      this.documentId === mb.documentId &&
      this.latitude === mb.latitude &&
      this.longitude === mb.longitude
    );
  }

  get key(): string | number | symbol {
    return this.id;
  }

  clone(): OrganizationFullModel {
    const cloned = new OrganizationFullModel();
    Object.assign(cloned, this);
    return cloned;
  }
}

