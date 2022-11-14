import {
  pageNumberProvider,
  pageSizeProvider,
  totalItemsCountProvider,
} from '../providers/pages.provider';

export function usePages() {
  const pageNumber = pageNumberProvider.inject();
  if (pageNumber.value == null) {
    throw new Error('Не удалось получить номер страницы');
  }

  const pageSize = pageSizeProvider.inject();
  if (pageSize.value == null) {
    throw new Error('Не удалось получить размер страницы');
  }

  const totalItemsCount = totalItemsCountProvider.inject();

  const changePage = ({ page }: { page: number }) => {
    if (pageNumber.value == null) {
      throw new Error('Не удалось получить номер страницы');
    }
    console.log({ page });

    pageNumber.value.value = page + 1;
  };

  return {
    pageNumber: pageNumber.value,
    pageSize: pageSize.value,
    totalItemsCount,
    changePage,
  };
}
