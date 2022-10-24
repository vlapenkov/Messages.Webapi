import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { SectionModel } from '../../models/section.model';

export const queries = defineHttpService<SectionModel>({
  url: 'sections',
});

export const getAll = queries.defineGet(() => ({
  url: '/list',
}));
