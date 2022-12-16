export interface IOrderPagedRequest {
  pageNumber: number;
  pageSize: number;
  producerId: number | undefined;
  organisationId: number | undefined;
}
