import { defineCollectionStore } from '@/app/core/services/harlem/custom-stores/collection/collection.store';
import { CollectionReadonlyState } from '@/app/core/services/harlem/state/custom/collection-readonly.state';
import { selected } from '@/app/core/services/harlem/state/decorators/selected-item.decorator';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { sectionsHttpService } from '../infrastructure/sections.http-service';
import { SectionModel } from '../models/section.model';

export class SectionState extends CollectionReadonlyState<SectionModel> {
  @selected({
    create: true,
    delete: false,
    update: false,
  })
  selectedItem: NotValidData<SectionModel> | null = null;
}

export const sectionsStore = defineCollectionStore(
  'sections',
  SectionModel,
  SectionState,
  sectionsHttpService,
);
