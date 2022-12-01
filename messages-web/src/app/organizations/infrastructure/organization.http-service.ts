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
