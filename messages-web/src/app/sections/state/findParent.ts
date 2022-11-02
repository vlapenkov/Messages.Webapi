import { SectionModel } from '../models/section.model';
import { sectionsStore } from './sections.store';

export function findParent(m: SectionModel) {
  return m.parentSectionId != null
    ? sectionsStore.items.value?.find((i) => i.id === m.parentSectionId)?.name ?? 'Не найден'
    : 'Отсутcтвует';
}
