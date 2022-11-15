import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';

export interface IproductsPageRequest extends IPagedRequest {
  catalogSectionId?: number;
  name: string | null;
}
