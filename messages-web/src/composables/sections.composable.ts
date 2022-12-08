import { SectionModel } from '@/app/sections/models/section.model';
import { sectionsStore } from '@/app/sections/state/sections.store';
import { CollectionStoreMixed } from '@/vue/base/presentational/state/collection/collection-state.vue';
import { WritableComputedRef } from 'vue';

export function useSections() {
  const sectionState = sectionsStore as CollectionStoreMixed;
  if (sectionState.items == null) {
    throw new Error('Что-то пошло не так');
  }
  const sections = sectionState.items({ force: false }) as WritableComputedRef<SectionModel[]>;
  return sections;
}
