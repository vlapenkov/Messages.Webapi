import { defineHttpService } from '@/app/core/services/http/define-http.service';

const { definePost, defineGet } = defineHttpService({
  url: 'api/WorkProducts',
});

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

const get = defineGet<unknown, number>((id) => ({ url: `/${id}` }));

export const productWorkHttpService = { post, get };
