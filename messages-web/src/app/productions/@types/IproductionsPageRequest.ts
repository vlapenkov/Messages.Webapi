import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { OrderByProduct } from '@/store/catalog-filters.store';

export enum ProductStatus {
  Draft,
  Active,
  Archive,
}

export interface IproductionsPageRequest extends IPagedRequest {
  catalogSectionId?: number;
  name: string | null;
  region: string | null;
  producerName: string | null;
  orderBy: OrderByProduct | null;
  status: ProductStatus.Active | null;
}
