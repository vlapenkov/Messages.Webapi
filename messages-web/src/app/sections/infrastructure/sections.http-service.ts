import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { Ok } from '@/app/core/handlers/http/results/ok.result';
import { defineCollectionService } from '@/app/core/services/http/custom/collection.http-service';
import type { ISectionModel } from '../models/section.model';

const [service, { defineGet, definePost }] = defineCollectionService<ISectionModel>({
  url: 'api/v1/Sections/',
});

service.get = defineGet(
  () => ({
    url: 'list',
  }),
  {
    append: [
      // useMock((): Partial<ISectionModel>[] => [
      //   {
      //     id: 1,
      //     name: 'Категория 1',
      //     parentSectionId: null,
      //   },
      //   {
      //     id: 2,
      //     name: 'Категория 1.2',
      //     parentSectionId: 1,
      //   },
      //   {
      //     id: 3,
      //     name: 'Категория 1.2.3',
      //     parentSectionId: 2,
      //   },
      //   {
      //     id: 4,
      //     name: 'Категория 2',
      //     parentSectionId: null,
      //   },
      //   {
      //     id: 5,
      //     name: 'Категория 2.1',
      //     parentSectionId: 4,
      //   },
      //   {
      //     id: 6,
      //     name: 'Категория 3',
      //     parentSectionId: null,
      //   },
      // ]),
    ],
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
