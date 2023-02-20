import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IAttribute } from '../@types/IAttribute';

const { defineGet } = defineHttpService<IAttribute>({
  url: 'api/Productions/attributes',
});

const get = defineGet();

export const attributeHttpService = { get };
