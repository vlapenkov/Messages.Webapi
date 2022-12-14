import { sectionsStore } from '@/app/sections/state/sections.store';
import { onMounted } from 'vue';

export function useSections() {
  onMounted(() => {
    if (sectionsStore.status.value.status === 'initial') {
      sectionsStore.loadSections();
    }
  });
  return {
    list: sectionsStore.sections,
    tree: sectionsStore.treeView,
  };
}
