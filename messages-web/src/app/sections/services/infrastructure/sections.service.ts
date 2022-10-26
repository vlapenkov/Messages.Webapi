import { extend } from '@/app/core/handlers/base/handler-lab';
import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { parseArray } from '@/app/core/services/http/handlers/parse.handlers';
import { ISectionModel, SectionModel } from '../../models/section.model';

export const { defineGet } = defineHttpService<ISectionModel>({
  url: 'sections',
});

export const getAll = extend(
  defineGet(() => ({
    url: '/list',
  })),
)
  .pipe(parseArray(SectionModel))
  .done();
