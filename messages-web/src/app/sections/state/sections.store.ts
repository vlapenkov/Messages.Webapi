import { defineCollectionStore } from '@/app/core/services/harlem/custom-stores/collection/collection.store';
import { CollectionEditableState } from '@/app/core/services/harlem/state/custom/collection-editable.state';
import { sectionsHttpService } from '../infrastructure/sections.http-service';
import { SectionModel } from '../models/section.model';

export class SectionState extends CollectionEditableState<SectionModel> {}

export const sectionsStore = defineCollectionStore(
  'sections',
  SectionModel,
  SectionState,
  sectionsHttpService,
);
