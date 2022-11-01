import { CollectionWithItemState } from '@/app/core/services/harlem/custom/collection/with-item/collection-with-item.state';
import { createCollectionWithItemUniqueStore } from '@/app/core/services/harlem/custom/collection/with-item/collection-with-ittem-unique.store';
import { SectionModel } from '../models/section.model';
import { sectionsHttpService } from '../infrastructure/sections.http-service';

export class SectionState extends CollectionWithItemState<SectionModel> {}

const [_, sectionsStore] = createCollectionWithItemUniqueStore(
  'sections',
  SectionModel,
  SectionState,
  sectionsHttpService,
);

export { sectionsStore };
