import {
  pageNumberProvider,
  pageSizeProvider,
  totalItemsCountProvider,
} from '../providers/pages.provider';

export function usePages() {
  const pageNumber = pageNumberProvider.inject();

  const pageSize = pageSizeProvider.inject();

  const totalItemsCount = totalItemsCountProvider.inject();

  const changePage = ({ page }: { page: number }) => {
    if (pageNumber.value == null) {
      return;
    }
    pageNumber.value.value = page + 1;
  };

  return {
    pageNumber: pageNumber.value,
    pageSize: pageSize.value,
    totalItemsCount,
    changePage,
  };
}
