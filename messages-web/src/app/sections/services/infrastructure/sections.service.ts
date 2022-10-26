import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { SectionModel } from '../../models/section.model';

export const { defineGet } = defineHttpService<SectionModel>({
  url: 'sections',
});

export const getAll = defineGet(() => ({
  url: '/list',
}));
