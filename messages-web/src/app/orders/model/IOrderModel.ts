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
}
