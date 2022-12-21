import { defineHttpService } from '@/app/core/services/http/define-http.service';

const { definePost, defineGet, definePut } = defineHttpService({
  url: 'api/WorkProducts',
});

export interface IWorkProductPutModel {
  id: number;
  catalogSectionId: number;
  name: string;
  fullName: string;
  description: string;
  price: number | null;
}

export interface IWorkProductPostModel {
  catalogSectionId: number;
  name: string;
  fullName: string;
  description: string;
  price: number | null;
  documents: {
    fileName: string;
    data: string;
    fileId: string;
  }[];
}

const post = definePost<number, IWorkProductPostModel>();

const put = definePut<void, IWorkProductPutModel>(({ id, ...rest }) => ({
  url: `/${id}`,
  bodyOrParams: rest as unknown as Record<string, unknown>,
}));

const get = defineGet<unknown, number>((id) => ({ url: `/${id}` }));

export const productWorkHttpService = { post, get, put };
