import { ref } from 'vue';
import { itemsCollectionProvider } from '../providers/items-collection.provider';

export function useItems() {
  const wrappedItems = itemsCollectionProvider.inject();

  return wrappedItems.value ? wrappedItems.value() : ref([]);
}
