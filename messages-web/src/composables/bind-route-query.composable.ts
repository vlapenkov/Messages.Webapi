import { useRouteQuery } from '@vueuse/router';

export function useRouteQueryBinded(queryName: string) {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const queryRef = useRouteQuery<string | null>(queryName);
}
