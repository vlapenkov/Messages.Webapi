import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IOrganizationFullModel } from '../@types/IOrganizationFullModel';

const { defineGet, definePost } = defineHttpService<IOrganizationFullModel>({
  url: 'api/Organizations',
});

const get = defineGet<IOrganizationFullModel, number>((id: number) => ({
  url: `/${id}`,
}));

const getByInn = defineGet<IOrganizationFullModel, string>((inn) => ({
  url: `/inn/${inn}`,
}));

const post = definePost<number, IOrganizationFullModel>();

export const organizationHttpService = { get, getByInn, post };

