import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IPagedRequest } from '@/app/core/services/http/@types/IPagedRequest';
import { IPagedResponse } from '@/app/core/services/http/@types/IPagedResponse';
import type { Action } from '@harlem/extension-action';
import { ComputedRef, WritableComputedRef } from 'vue';
import { DataStatus } from '../../../tools/data-status';

export interface IPageableCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> {
  pageSize: WritableComputedRef<number>;
  pageNumber: WritableComputedRef<number>;
  pages: WritableComputedRef<IPagedResponse<TModel>[]>;
  status: WritableComputedRef<DataStatus>;
  toNextPage: (payload?: unknown) => void;
  toPreviousPage: (payload?: unknown) => void;
  currentPage: ComputedRef<IPagedResponse<TModel> | undefined>;
  currentPageItems: WritableComputedRef<TModel[] | null>;
  getPage: Action<IPagedRequest>;
}
