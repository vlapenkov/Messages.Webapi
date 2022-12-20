import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { http } from '@/app/core/services/http/axios/axios.service';
import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IproductionsPageRequest } from '../@types/IproductionsPageRequest';
import { IProductionModel } from '../models/production.model';

const { defineGet, defineDelete, definePatch } = defineHttpService({
  url: 'api/productions',
});

const getPage = defineGet<IPagedResponse<IProductionModel>, IproductionsPageRequest>();

const remove = defineDelete<void, number>((id) => ({
  url: `/${id}`,
}));

export enum ProductionStatus {
  Draft,
  Active,
  Archive,
}

export interface ISetProdstionStatusArg {
  id: number;
  status: ProductionStatus;
}

const setStatus = definePatch<void, ISetProdstionStatusArg>(({ id, status }) => ({
  url: `/${id}/status`,
  bodyOrParams: { status },
}));

/** Получить атрибуты всей продукции */
const getAttributes = defineGet<{ id: number; name: string }[]>();

const updateStatus = async (id: number, status: number) => {
  const response = await http.patch(`api/Productions/${id}/status`, status, {
    headers: {
      'accept': '*/*',
      'Content-Type': 'application/json',
    },
  });
  return response;
};

export const productionsHttpService = {
  getPage,
  getAttributes,
  remove,
  setStatus,
  updateStatus,
};

