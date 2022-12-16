import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IOrganizationFullModel } from '../@types/IOrganizationFullModel';

const { defineGet, definePost, definePatch } = defineHttpService<IOrganizationFullModel>({
  url: 'api/Organizations',
});

const get = defineGet<IOrganizationFullModel, number>((id: number) => ({
  url: `/${id}`,
}));

const post = definePost<number, IOrganizationFullModel>();

export interface ISetOrganizationStatusArg {
  id: number;
  status: number;
}

const setStatus = definePatch<void, ISetOrganizationStatusArg>(({ id, status }) => ({
  url: `/${id}/status`,
  bodyOrParams: status,
}));

export const organizationHttpService = { get, post, setStatus };
