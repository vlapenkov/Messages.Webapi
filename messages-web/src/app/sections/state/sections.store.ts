import { CollectionEditableState } from '@/app/core/services/harlem/custom-stores/collection/editable/collection-editable.state';
import { createCollectionEditableStore } from '@/app/core/services/harlem/custom-stores/collection/editable/collection-editable.store';
import { sectionsHttpService } from '../infrastructure/sections.http-service';
import { SectionModel } from '../models/section.model';

export class SectionState extends CollectionEditableState<SectionModel> {}

const [_, sectionsStore] = createCollectionEditableStore(
  'sections',
  SectionModel,
  SectionState,
  sectionsHttpService,
);

export { sectionsStore };
