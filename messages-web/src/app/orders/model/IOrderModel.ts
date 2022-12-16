import { IModel } from '@/app/core/models/@types/IModel';

export interface IOrderModel extends IModel {
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
  organisationName: string;
  userName: string;
  comments: string;
  sum: number;
  quantity: number;
  id: number;
  statusText: string;
  producerName: string;
}

export interface IOrderModelFull extends IModel {
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
  id: number;
  organisationName: string;
  userName: string;
  comments: string;
  statusText: string;
  orderItems: [
    {
      productId: number;
      productName: string;
      documentId: string;
      price: number;
      quantity: number;
      sum: number;
    },
  ];
  sum: number;
}

