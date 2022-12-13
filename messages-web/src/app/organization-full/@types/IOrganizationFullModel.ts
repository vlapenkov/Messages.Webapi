import { IModel } from '@/app/core/models/@types/IModel';

export interface IOrganizationFullModel extends IModel {
  id?: number;
  name: string;
  fullName: string;
  ogrn: string;
  inn: string;
  kpp: string;
  region: string;
  city: string;
  address: string;
  factAddress: string;
  site: string;
  okved: string;
  okved2: string;
  phone: string;
  email: string;
  latitude: number;
  longitude: number;
  isProducer: boolean;
  isBuyer: boolean;
  bankName: string;
  account: string;
  corrAccount: string;
  bik: string;
  document?: {
    fileName: string;
    data: string;
    fileId: string;
  };
  statusText?: string;
  lastModified?: string;
  lastModifiedBy?: string;
  created?: string;
  createdBy?: string;
}
