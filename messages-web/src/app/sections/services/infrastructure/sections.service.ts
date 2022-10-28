import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { ISectionModel } from '../../models/section.model';

const [service, { defineGet }] = defineCollectionService<ISectionModel>({
  url: 'api/v1/Sections/',
});

service.get = defineGet(() => ({
  url: 'list',
}));

export const sectionsHttpService = service;
