import { IproductionsPageRequest } from '@/app/productions/@types/IproductionsPageRequest';
import { productionsStore } from '@/app/productions/state/productions.store';
import { watch, computed, WatchSource, ref } from 'vue';

export function useProductions(source: WatchSource<IproductionsPageRequest>) {
  const { getPageState, loadPage } = productionsStore;
  const lastRequest = ref<IproductionsPageRequest>();
  watch(
    source,
    (r) => {
      loadPage(r);
    },
    {
      immediate: true,
    },
  );

  const pageState = computed(() =>
    lastRequest.value ? getPageState(lastRequest.value) : undefined,
  );
}
