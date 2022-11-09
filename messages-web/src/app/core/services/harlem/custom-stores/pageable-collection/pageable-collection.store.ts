import { Constructor } from '@/app/@types/constructor';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import type { Action } from '@harlem/extension-action';
import { computed, watch } from 'vue';
import { IPagedRequest } from '../../../http/@types/IPagedRequest';
import { IPagedResponse } from '../../../http/@types/IPagedResponse';
import { IPageableCollectionHttpServie } from '../../../http/custom/pageable-collection.http-service';
import { DefaultStore, createDefaultStore } from '../../harlem.service';
import { StateBase } from '../../state/base/state-base';
import { getDataStatusProp } from '../../state/decorators/property-keys/data-status.prop-key';
import { getPageNumberProp } from '../../state/decorators/property-keys/page-number.prop-key';
import { getPageRequestProp } from '../../state/decorators/property-keys/page-request.prop-key';
import { getPageSizeProp } from '../../state/decorators/property-keys/page-size.prop-key';
import { getPagesProp } from '../../state/decorators/property-keys/pages.prop-key';
import { DataStatus } from '../../tools/data-status';
import { IPageableCollectionStore } from './@types/IPageableCollectionStore';

export function definePageableCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends StateBase,
>(
  name: string,
  Model: Constructor<TModel>,
  State: Constructor<TState>,
  service: IPageableCollectionHttpServie<TIModel>,
) {
  const stateDefault = new State();
  const store: DefaultStore<TState> = createDefaultStore(name, stateDefault);

  const { computeState, mutation, getter, action } = store;

  const pagesKey = getPagesProp(stateDefault);
  const pageNumberKey = getPageNumberProp(stateDefault);
  const pageSizeKey = getPageSizeProp(stateDefault);
  if (pagesKey == null || pageNumberKey == null || pageSizeKey == null) {
    throw new Error(
      'Cannot create pageable collection props without pages, pagenumber and pageSize',
    );
  }

  const pages = computeState((state) => state[pagesKey] as unknown as IPagedResponse<TModel>[]);
  const pageNumber = computeState((state) => state[pageNumberKey] as unknown as number);
  const pageSize = computeState((state) => state[pageSizeKey] as unknown as number);

  const dataStatusKey = getDataStatusProp(stateDefault);

  if (dataStatusKey == null) {
    throw new Error('Please, provide @dataStatus for collection state');
  }

  const status = computeState((state) => state[dataStatusKey] as unknown as DataStatus);

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
    get: () => currentPage.value?.rows,
    set: (v) => {
      if (currentPage.value == null || v == null) {
        return;
      }
      currentPage.value.rows = v;
    },
  });

  const getPage: Action<IPagedRequest> = action('get-current-page', async (payload) => {
    status.value = new DataStatus('loading');
    const response = await service.getPage({
      pageNumber: payload.pageNumber,
      pageSize: payload.pageSize,
    });
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

  const readonlyStore: IPageableCollectionStore<TIModel, TModel> = {
    pageSize,
    pageNumber,
    pages,
    status,
    toNextPage,
    toPreviousPage,
    currentPage,
    currentPageItems,
  };

  const pageRequestKey = getPageRequestProp(stateDefault);

  const pageRequest =
    pageRequestKey == null
      ? getter<IPagedRequest>('page-request', () => ({
          pageNumber: pageNumber.value,
          pageSize: pageSize.value,
        }))
      : getter<IPagedRequest>('page-request', (state) => {
          const getRequest = state[pageRequestKey as keyof typeof state] as unknown as (
            arg: IPageableCollectionStore<TIModel, TModel>,
          ) => IPagedRequest;
          return getRequest(readonlyStore);
        });
  watch(
    pageRequest,
    (request) => {
      console.log({ request });

      if (request.pageSize <= 0) {
        return;
      }
      if (currentPage.value == null) {
        getPage(request);
      }
    },
    {
      immediate: true,
    },
  );
  return readonlyStore;
}
