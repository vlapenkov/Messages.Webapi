import { IModel } from '@/app/core/models/@types/IModel';

export interface IOrganizationModel extends IModel {
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
  id: number;
  name: string;
  fullName: string;
  ogrn: string;
  inn: string;
  kpp: string;
  region: string;
  city: string;
  address: string;
  site: string;
  okved: string;
  okved2: string;
  statusText: string;
}
