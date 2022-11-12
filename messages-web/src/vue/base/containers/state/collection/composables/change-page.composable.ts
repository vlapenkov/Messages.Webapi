import { pageNumberProvider } from '../providers/pages.provider';

export function useChangePage() {
  const pageNumber = pageNumberProvider.inject();

  const changePage = ({ page }: { page: number }) => {
    pageNumber.value = page + 1;
  };

  return changePage;
}
