import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { ProductionsOrder } from '@/store/catalog-filters.store';

export enum ProductStatus {
  Draft,
  Active,
  Archive,
}

export interface IproductionsPageRequest extends IPagedRequest {
  catalogSectionId: number | null;
  name: string | null;
  region: string | null;
  producerName: string | null;
  ProducerId: number | null;
  orderBy: ProductionsOrder | null;
  status: ProductStatus.Active | null;
}
