import { defineRepository } from '@/app/core/repositories/http-repository-base';
import { SectionModel } from '../models/section.model';

export const queries = defineRepository<SectionModel>({
  url: 'sections',
});

export const getAll = queries.defineGet(() => ({
  url: '/list',
}));
