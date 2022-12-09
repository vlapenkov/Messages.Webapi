import { IModel } from '@/app/core/models/@types/IModel';

export interface IOrganizationFullMiodel extends IModel {
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
  lastModified: string;
  lastModifiedBy: string;
  created: string;
  createdBy: string;
  latitude: number | null;
  longitude: number | null;
}
