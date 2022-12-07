import { defineCollectionStore } from '@/app/core/services/harlem/custom-stores/collection/collection.store';
import { sectionsHttpService } from '../infrastructure/sections.http-service';
import { SectionModel } from '../models/section.model';
import { SectionState } from './sections.state';

const [sectionsStore] = defineCollectionStore(
  'catalog',
  SectionModel,
  SectionState,
  sectionsHttpService,
);

export { sectionsStore };
