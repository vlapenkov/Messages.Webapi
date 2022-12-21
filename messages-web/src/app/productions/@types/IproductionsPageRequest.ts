import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';

export enum OrderByProduct {
  NameByAsc = 0,
  NameByDesc,
  RegionByAsc,
  RegionByDesc,
  ProducerByAsc,
  ProducerByDesc,
  RatingByAsc,
  RatingByDesc,
}

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
