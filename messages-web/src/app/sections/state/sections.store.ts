import { CollectionReadonlyState } from '@/app/core/services/harlem/custom/collection/readonly/collection-readonly.state';
import { createCollectionReadonlyStore } from '@/app/core/services/harlem/custom/collection/readonly/collection-readonly.store';
import { SectionModel } from '../models/section.model';
import { sectionsHttpService } from '../infrastructure/sections.http-service';

export class SectionState extends CollectionReadonlyState<SectionModel> {}

const [_, sectionsStore] = createCollectionReadonlyStore(
  'sections',
  SectionModel,
  SectionState,
  sectionsHttpService,
);

export { sectionsStore };
