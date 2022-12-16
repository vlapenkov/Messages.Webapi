import { http } from '@/app/core/services/http/axios/axios.service';
import { definePageableCollectionService } from '@/app/core/services/http/custom/pageable-collection.http-service';
import { IOrganizationModel } from '../model/IOrganizationModel';

const [organizationsHttpService, { defineGet }] =
  definePageableCollectionService<IOrganizationModel>({
    url: 'api/Organizations',
  });

export { organizationsHttpService };

export const getOrganization = defineGet<IOrganizationModel, number>((id) => ({
  url: `/${id}`,
}));

export const updateStatus = async (id: number, status: number) => {
  const response = await http.patch(`api/Organizations/${id}/status`, status, {
    headers: {
      'accept': '*/*',
      'Content-Type': 'application/json',
    },
  });
  return response;
};
