import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
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

export const ordersHttpService = { postOrder, getOrder, getPage };
