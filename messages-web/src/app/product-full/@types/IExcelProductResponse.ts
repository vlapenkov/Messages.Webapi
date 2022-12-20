import { IModel } from '@/app/core/models/@types/IModel';

export interface IExcelProductResponse extends IModel {
  name: string;
  fullName: string;
  catalogSectionId: number;
  description: string;
  codeTnVed: string;
  codeOkpd2: string;
  address: string;
  price: number;
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

