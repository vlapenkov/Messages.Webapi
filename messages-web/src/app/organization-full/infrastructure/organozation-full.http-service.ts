import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IOrganizationFullModel } from '../@types/IOrganizationFullModel';

const { defineGet, definePost } = defineHttpService<IOrganizationFullModel>({
  url: 'api/Organizations',
});

const get = defineGet<IOrganizationFullModel, number>((id: number) => ({
  url: `/${id}`,
}));

export interface IOrganizationFullPostModel {
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
  latitude: number;
  longitude: number;
}

const post = definePost<number, IOrganizationFullPostModel>();

export const organizationHttpService = { get, post };
