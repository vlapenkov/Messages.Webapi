import { defineSingleHttpService } from '@/app/core/services/http/custom/single.http-service';
import { IOrganizationFullMiodel } from '../@types/IOrganizationFullModel';

const [organizationFullService, { defineGet }] = defineSingleHttpService<IOrganizationFullMiodel>({
  url: 'api/Organizations',
});

organizationFullService.get = defineGet((arg: { id: number }) => ({
  url: `/${arg.id}`,
}));

export { organizationFullService };
