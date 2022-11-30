import { defineCollectionStore } from '@/app/core/services/harlem/custom-stores/collection/collection.store';
import { organizationsHttpService } from '../infrastructure/organization.http-service';
import { OrganizationModel } from '../model/organization.model';
import { OrganizationsState } from './organizations.state';

export const [organizationStore] = defineCollectionStore(
  'organizations',
  OrganizationModel,
  OrganizationsState,
  organizationsHttpService,
);
