export interface IPagedResponse<TModel> {
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
