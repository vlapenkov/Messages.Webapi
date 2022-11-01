import { CollectionEditableState } from '@/app/core/services/harlem/custom/collection/editable/collection-editable.state';
import { createCollectionEditableStore } from '@/app/core/services/harlem/custom/collection/editable/collection-editable.store';
import { SectionModel } from '../models/section.model';
import { sectionsHttpService } from '../infrastructure/sections.http-service';

export class SectionState extends CollectionEditableState<SectionModel> {}

const [_, sectionsStore] = createCollectionEditableStore(
  'sections',
  SectionModel,
  SectionState,
  sectionsHttpService,
);

export { sectionsStore };
