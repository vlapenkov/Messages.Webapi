import { IModel } from '@/app/core/models/@types/IModel';

export interface IPagedResponse<TModel extends IModel> {
  firstItemOnPage: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
  isFirstPage: boolean;
  isLastPage: boolean;
  lastItemOnPage: number;
  pageCount: number;
  pageNumber: number;
  pageSize: number;
  totalItemCount: number;
  rows: TModel[];
}
