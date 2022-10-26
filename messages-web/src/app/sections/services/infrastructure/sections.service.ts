import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { ISectionModel } from '../../models/section.model';

export const { defineGet } = defineHttpService<ISectionModel>({
  url: 'sections',
});

export const getAll = defineGet(() => ({
  url: '/list',
}));
