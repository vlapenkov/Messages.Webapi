import { sectionsStore } from '@/app/sections/state/sections.store';
import { onMounted } from 'vue';

export function useSections() {
  if (sectionsStore.sections == null) {
    throw new Error('Что-то пошло не так');
  }
  onMounted(() => {
    if (sectionsStore.status.value.status !== 'loaded') {
      sectionsStore.loadSections();
    }
  });
  return {
    list: sectionsStore.sections,
    tree: sectionsStore.treeView,
  };
}
