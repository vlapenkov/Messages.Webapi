import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IExcelProductResponse } from '../@types/IExcelProductResponse';
import { IProductFullModel } from '../@types/IProductFullModel';

const { defineGet, definePost, definePut } = defineHttpService<IProductFullModel>({
  url: 'api/Products',
});

const get = defineGet<IProductFullModel, number>((id) => ({
  url: `/${id}`,
}));

export interface IProductPostModel {
  catalogSectionId: number;
  name: string;
  fullName: string;
  description: string;
  codeTnVed: string;
  codeOkpd2: string;
  address: string;
  price: number | null;
  article: string;
  attributeValues: {
    attributeId: number;
    value: string;
  }[];
  documents: {
    fileName: string;
    data: string;
    fileId: string;
  }[];
}

const post = definePost<number, IProductPostModel>();

export interface IProductPutModel {
  id: number;
  catalogSectionId: number;
  name: string;
  fullName: string;
  description: string;
  price: number | null;
  codeTnVed: string;
  codeOkpd2: string;
  address: string;
  attributeValues: {
    attributeId: number;
    value: string;
  }[];
}

export const put = definePut<void, IProductPutModel>(({ id, ...rest }) => ({
  bodyOrParams: { ...rest },
  url: `/${id}`,
}));

const fromExcel = definePost<IExcelProductResponse[], { fileName: string; data: string }>((v) => ({
  url: `/fromexcel`,
  bodyOrParams: v,
}));

export const productFullHttpService = { get, post, put, fromExcel };
