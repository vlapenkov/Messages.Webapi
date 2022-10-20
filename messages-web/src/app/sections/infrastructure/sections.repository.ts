import { defineRepository } from '@/app/core/repositories/http-repository-base';
import { SectionModel } from '../models/section.model';

export const sectionsRepository = defineRepository<SectionModel>({
  setup: ({ get }) => {
    const result = {
      getAll: get<SectionModel[]>(() => 'list'),
    };
    return result;
  },
  url: 'sections',
});
