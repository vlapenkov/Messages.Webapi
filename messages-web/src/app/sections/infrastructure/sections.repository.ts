import { defineRepository } from '@/app/core/repositories/http-repository-base';
import { SectionModel } from '../models/section.model';

export const { get } = defineRepository<SectionModel>({
  url: 'sections',
});

export const getAll = get<SectionModel[]>(() => ({
  url: 'list',
}));
