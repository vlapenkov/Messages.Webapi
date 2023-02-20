import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { ISectionModel } from '../models/ISectionModel';

const [service, { defineGet, definePost }] = defineCollectionService<ISectionModel>({
  url: 'api/Sections/',
});

service.get = defineGet(() => ({
  url: 'list',
}));

const post = definePost<number, { parentSectionId: number | null; name: string }>(
  ({ parentSectionId, name }) => ({
    bodyOrParams: { parentSectionId, name },
  }),
);

export const sectionsHttpService = { ...service, post };
