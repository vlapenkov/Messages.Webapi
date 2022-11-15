import { IModel } from '@/app/core/models/@types/IModel';

export interface IProductFullModel extends IModel {
  id: number;
  name: string;
  fullName: string;
  catalogSectionId: number;
  description: string;
  codeTnVed: string;
  price: number;
  measuringUnit: string;
  country: string;
  currency: string;
  status: number;
  attributeValues: {
    baseProductId: number;
    attributeId: number;
    value: string;
  }[];
  documents: {
    fileName: string;
    data: string;
    fileId: string;
  }[];
}
