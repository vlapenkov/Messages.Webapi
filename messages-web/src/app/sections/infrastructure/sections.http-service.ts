import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { Ok } from '@/app/core/handlers/http/results/ok.result';
import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import { ISectionModel } from '../models/ISectionModel';

const [service, { defineGet, definePost }] = defineCollectionService<ISectionModel>({
  url: 'api/v1/Sections/',
});

// const fakeSections = new FakeRepository(SectionModel, 15);

service.get = defineGet(
  () => ({
    url: 'list',
  }),
  {
    // append: [useMock(() => fakeSections.collection)],
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
      // useMock<ISectionModel, ISectionModel>((request) => {
      //   const mock = new SectionModel().mock() as SectionModel;
      //   mock.parentSectionId = request.parentSectionId;
      //   mock.name = request.name;
      //   fakeSections.add(mock);
      //   return request;
      // }),
    ],
  },
);

export const sectionsHttpService = service;
