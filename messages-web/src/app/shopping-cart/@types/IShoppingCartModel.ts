import { IModel } from '@/app/core/models/@types/IModel';

export interface IShoppingCartModel extends IModel {
  productId: number;
  productName: string;
  documentId: string;
  price: number;
  quantity: number;
  sum: number;
}
