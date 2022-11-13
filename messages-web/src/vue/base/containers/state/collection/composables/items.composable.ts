import { itemsCollectionProvider } from '../providers/items-collection.provider';

export function useItems() {
  const wrappedItems = itemsCollectionProvider.inject();
  if (wrappedItems.value == null) {
    throw new Error('невозможно показать коллекцию элементов');
  }

  return wrappedItems.value();
}
