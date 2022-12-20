import { defineHttpService } from '@/app/core/services/http/define-http.service';

const { definePost } = defineHttpService({
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

export const productWorkHttpService = { post };
