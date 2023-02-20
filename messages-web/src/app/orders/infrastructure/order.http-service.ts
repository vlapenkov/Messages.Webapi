import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import { http } from '@/app/core/services/http/axios/axios.service';
import { defineHttpService } from '@/app/core/services/http/define-http.service';
import { IOrderModel, IOrderModelFull } from '../model/IOrderModel';

const { defineGet, definePost } = defineHttpService<IOrderModel>({
  url: 'api/Orders',
});

const getOrder = defineGet<IOrderModelFull, number>((id) => ({
  url: `/${id}`,
}));

const postOrder = definePost<number, undefined>();

const getPage = defineGet<IPagedResponse<IOrderModel>, IPagedRequest>();

const updateStaus = async (id: number, status: number) => {
  const response = await http.patch(`api/Orders/${id}/status`, status, {
    headers: {
      'accept': '*/*',
      'Content-Type': 'application/json',
    },
  });
  return response;
};

export const ordersHttpService = { postOrder, getOrder, getPage, updateStaus };

