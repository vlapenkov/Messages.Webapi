import { defineHttpService } from '@/app/core/services/http/define-http.service';

const { definePost, defineGet } = defineHttpService({
  url: 'api/ServiceProducts',
});

export interface IServiceProductPostModel {
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

export interface IServiceProduct {
  id: number;
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
  name: string;
  fullName: string;
  catalogSectionId: number;
  description: string;
  price: number;
  statusText: string;
  organization: {
    id: number;
    createdBy: string;
    lastModifiedBy: string;
    created: string;
    lastModified: string;
    name: string;
    region: string;
    latitude: number;
    longitude: number;
    isProducer: true;
    isBuyer: true;
  };
  documents: {
    fileName: string;
    data: string;
    fileId: string;
  }[];
}

const post = definePost<number, IServiceProductPostModel>();

const get = defineGet<IServiceProduct, number>((id) => ({ url: `/${id}` }));

export const productServiceHttpService = { post, get };
