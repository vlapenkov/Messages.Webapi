import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { Ok } from '@/app/core/handlers/http/results/ok.result';
import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { FakeRepository } from '@/app/core/services/http/handlers/mock/fake-repository';
import { useMock } from '@/app/core/services/http/wrappers/useMock.wrapper';
import { ISectionModel, SectionModel } from '../models/section.model';

const [service, { defineGet, definePost }] = defineCollectionService<ISectionModel>({
  url: 'api/v1/Sections/',
});

const fakeSections = new FakeRepository(SectionModel, 15);

service.get = defineGet(
  () => ({
    url: 'list',
  }),
  {
    append: [useMock(() => fakeSections.collection)],
  },
);

service.post = definePost(
  ({ parentSectionId, name }) => ({
    bodyOrParams: { parentSectionId, name },
  }),
  {
    append: [
      (handler) => async (request) => {
        const response = await handler(request);
        if (response.status === HttpStatus.Success) {
          return new Ok({ ...request, id: response.data });
        }
        return response;
      },
    ],
  },
);

export const sectionsHttpService = service;
