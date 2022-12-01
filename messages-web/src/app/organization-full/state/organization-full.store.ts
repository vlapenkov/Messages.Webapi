import { defineSingleItemStore } from '@/app/core/services/harlem/custom-stores/single-Item/single-item.store';
import { organizationFullService } from '../infrastructure/organozation-full.http-service';
import { OrganizationFullModel } from '../models/organozation-full.model';
import { OrganizationFullState } from './organization-full.state';

const [organizationFullStore] = defineSingleItemStore(
  'organization-full',
  OrganizationFullModel,
  OrganizationFullState,
  organizationFullService,
);

export { organizationFullStore };
