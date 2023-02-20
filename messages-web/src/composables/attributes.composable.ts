import { attributeStore } from '@/app/attributes/state/attribute.store';
import { onMounted } from 'vue';

export function useAttributes() {
  const { items, getAsync, status } = attributeStore;
  onMounted(() => {
    if (status.value.status === 'initial') {
      getAsync();
    }
  });
  return items;
}
