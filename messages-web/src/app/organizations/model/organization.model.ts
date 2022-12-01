import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { description } from '@/app/core/models/decorators/description.decorator';
import { render } from '@/app/core/models/decorators/render.decorator';
import { IOrganizationModel } from './IOrganizationModel';

export class OrganizationModel extends ModelBase<IOrganizationModel> {
  id = -1;

  @description('Создал')
  createdBy = '';

  @description('Внёс последние изменения')
  lastModifiedBy = '';

  @description('Дата создания')
  @render((m: OrganizationModel) => (m.created ? m.created.toLocaleDateString() : 'неизвестна'))
  created: Date | null = null;

  @description('Дата последних изменений')
  @render((m: OrganizationModel) => (m.created ? m.created.toLocaleDateString() : 'неизвестна'))
  lastModified: Date | null = null;

  @description('Имя')
  name = '';

  @description('Полное имя')
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

  @description('Сайт')
  site = '';

  @description('ОКВЭД')
  okved = '';

  @description('ОКВЭД 2')
  okved2 = '';

  statusText = '';

  fromResponse(model: IOrganizationModel): boolean {
    try {
      this.id = model.id;
      this.createdBy = model.createdBy;
      this.lastModifiedBy = model.lastModifiedBy;
      this.created = new Date(model.created);
      this.lastModified = new Date(model.lastModified);
      this.name = model.name;
      this.fullName = model.fullName;
      this.ogrn = model.ogrn;
      this.inn = model.inn;
      this.kpp = model.kpp;
      this.region = model.region;
      this.city = model.city;
      this.address = model.address;
      this.site = model.site;
      this.okved = model.okved;
      this.okved2 = model.okved2;
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IOrganizationModel {
    return {
      id: this.id,
      created: this.created?.toUTCString() ?? '',
      createdBy: this.createdBy,
      lastModified: this.lastModified?.toUTCString() ?? '',
      lastModifiedBy: this.lastModifiedBy,
      name: this.name,
      fullName: this.fullName,
      ogrn: this.ogrn,
      inn: this.inn,
      kpp: this.kpp,
      region: this.region,
      city: this.city,
      address: this.address,
      site: this.site,
      okved: this.okved,
      okved2: this.okved2,
      statusText: this.statusText,
      [modelMarker]: this[modelMarker],
    };
  }

  equals(mb: OrganizationModel): boolean {
    return this.key === mb.key;
  }

  get key(): string {
    return JSON.stringify(this.toRequest());
  }

  clone(): ModelBase<IModel> {
    const nm = new OrganizationModel();
    Object.assign(nm, this);
    return nm;
  }
}
