import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';

export interface IproductionsPageRequest extends IPagedRequest {
  catalogSectionId?: number;
  name: string | null;
  region: string | null;
  producerName: string | null;
  orderBy: number | null;
}
