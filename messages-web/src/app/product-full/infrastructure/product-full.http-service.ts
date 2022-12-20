import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IExcelProductResponse } from '../@types/IExcelProductResponse';
import { IProductFullModel } from '../@types/IProductFullModel';

const { defineGet, definePost, definePut } = defineHttpService<IProductFullModel>({
  url: 'api/Products',
});

const get = defineGet<IProductFullModel, number>((id) => ({
  url: `/${id}`,
}));

const post = definePost<number, IProductFullModel>((md) => ({
  bodyOrParams: {
    catalogSectionId: md.catalogSectionId,
    name: md.name,
    fullName: md.fullName,
    description: md.description,
    attributeValues: md.attributeValues,
    codeTnVed: md.codeTnVed,
    price: md.price,
    documents: md.documents,
  },
}));

export interface IProductPutModel {
  id: number;
  catalogSectionId: number;
  name: string;
  fullName: string;
  description: string;
  price: number;
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

export const productFullService = { get, post, put, fromExcel };

