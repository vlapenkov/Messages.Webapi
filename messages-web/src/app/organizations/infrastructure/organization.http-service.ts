import { http } from '@/app/core/services/http/axios/axios.service';
import { definePageableCollectionService } from '@/app/core/services/http/custom/pageable-collection.http-service';
import { IOrganizationModel } from '../model/IOrganizationModel';

const [service, { defineGet }] = definePageableCollectionService<IOrganizationModel>({
  url: 'api/Organizations',
});

const getOrganization = defineGet<IOrganizationModel, number>((id) => ({
  url: `/${id}`,
}));

const updateStatus = async (id: number, status: number) => {
  const response = await http.patch(`api/Organizations/${id}/status`, status, {
    headers: {
      'accept': '*/*',
      'Content-Type': 'application/json',
    },
  });
  return response;
};

export const organizationsHttpService = { ...service, getOrganization, updateStatus };
