import { Constructor } from '@/app/@types/constructor';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { computed, DeepReadonly, watch, WritableComputedRef } from 'vue';
import { IPagedRequest } from '../../../http/@types/IPagedRequest';
import { IPagedResponse } from '../../../http/@types/IPagedResponse';
import { IPageableCollectionHttpServie } from '../../../http/custom/pageable-collection.http-service';
import { DefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getPageNumberProp } from '../../state/decorators/property-keys/page-number.prop-key';
import { getPageRequestProp } from '../../state/decorators/property-keys/page-request.prop-key';
import { getPageSizeProp } from '../../state/decorators/property-keys/page-size.prop-key';
import { getPagesProp } from '../../state/decorators/property-keys/pages.prop-key';
import { DataStatus } from '../../tools/data-status';

export function usePages<
  TState extends StateBase,
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
>(
  { state: stateDefault, computeState, mutation, getter, action }: DefaultStore<TState>,
  Model: Constructor<TModel>,
  service: IPageableCollectionHttpServie<TIModel>,
  status: WritableComputedRef<DataStatus>,
) {
  const pagesKey = getPagesProp(stateDefault);
  const pageNumberKey = getPageNumberProp(stateDefault);
  const pageSizeKey = getPageSizeProp(stateDefault);
  if (pagesKey == null || pageNumberKey == null || pageSizeKey == null) {
    throw new Error(
      'Cannot create pageable collection props without pages, pagenumber and pageSize',
    );
  }

  const pages = computeState(
    (state) => state[pagesKey as keyof TState] as unknown as IPagedResponse<TModel>[],
  );
  const pageNumber = computeState(
    (state) => state[pageNumberKey as keyof TState] as unknown as number,
  );
  const pageSize = computeState((state) => state[pageSizeKey as keyof TState] as unknown as number);

  const toNextPage = mutation('next-page', () => {
    pageNumber.value += 1;
  });

  const toPreviousPage = mutation('prev-page', () => {
    pageNumber.value -= 1;
  });

  const currentPage = getter('get-current-page', () =>
    pages.value.find((p) => p.pageNumber === pageNumber.value && p.pageSize === pageSize.value),
  );

  const currentPageItems = computed({
    get: () => currentPage.value?.rows ?? null,
    set: (v) => {
      if (currentPage.value == null || v == null) {
        return;
      }
      currentPage.value.rows = v;
    },
  });

  const getPage: Action<IPagedRequest> = action('get-current-page', async (payload) => {
    status.value = new DataStatus('loading');
    const response = await service.getPage(payload);
    if (response.status !== HttpStatus.Success) {
      status.value = new DataStatus('error', response.message);
      return;
    }
    if (response.data == null) {
      return;
    }
    const parsedData = response.data.rows.map((r) => {
      const md = new Model();
      const success = md.fromResponse(r);
      if (!success) {
        status.value = new DataStatus(
          'error',
          'Не удалось сопоставить элементы страницы с классом модели',
        );
        throw new Error('Не удалось сопоставить элементы страницы с классом модели');
      }
      return md;
    });

    const pageToAdd = { ...response.data, rows: parsedData };

    const indexOfCurrentPage = pages.value.findIndex(
      (p) => p.pageNumber === payload.pageNumber && p.pageSize === payload.pageSize,
    );
    const newPages = [...pages.value];
    if (indexOfCurrentPage < 0) {
      newPages.push(pageToAdd);
    } else {
      newPages[indexOfCurrentPage] = pageToAdd;
    }
    pages.value = newPages;
    status.value = new DataStatus('loaded');
  });

  const pageRequestKey = getPageRequestProp(stateDefault);

  const readonlyStore = {
    pages,
    pageNumber,
    pageSize,
    toNextPage,
    toPreviousPage,
    currentPage,
    currentPageItems,
    getPage,
  };

  const pageRequest =
    pageRequestKey == null
      ? getter<IPagedRequest>('page-request', () => ({
          pageNumber: pageNumber.value,
          pageSize: pageSize.value,
        }))
      : getter<IPagedRequest>('page-request', (state) => {
          const getRequest = state[pageRequestKey as keyof typeof state] as unknown as (
            arg: DeepReadonly<TState>,
          ) => IPagedRequest;
          return getRequest(state);
        });
  watch(
    pageRequest,
    (request) => {
      console.log({ request });

      if (request.pageSize <= 0) {
        return;
      }
      getPage(request);
      // if (currentPage.value == null) {
      // }
    },
    {
      immediate: true,
    },
  );

  return readonlyStore;
}
