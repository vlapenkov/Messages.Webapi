import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { IOrganizationModel } from '../model/IOrganizationModel';

const [organizationsHttpService, { defineGet }] = defineCollectionService<IOrganizationModel>({
  url: 'api/Organizations',
});

export { organizationsHttpService };

export const getOrganization = defineGet<IOrganizationModel, number>((id) => ({
  url: `/${id}`,
}));
