import { defineRepository } from '@/app/core/repositories/http-repository-base';
import { SectionModel } from '../models/section.model';

export const { defineGet } = defineRepository<SectionModel>({
  url: 'sections',
});

export const getAll = defineGet(() => ({
  url: '/list',
}));
