import { ICollectionStoreRead } from '@/app/core/services/harlem/custom-stores/collection/@types/ICollectionStoreRead';
import { ISelectedItem } from '@/app/core/services/harlem/custom-stores/collection/@types/IStoreSelectedItem';
import { defineCollectionStore } from '@/app/core/services/harlem/custom-stores/collection/collection.store';
import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { collection } from '@/app/core/services/harlem/state/decorators/collection.decorator';
import { dataStatus } from '@/app/core/services/harlem/state/decorators/data-status.decorator';
import { selected } from '@/app/core/services/harlem/state/decorators/selected-item.decorator';
import { treeView } from '@/app/core/services/harlem/state/decorators/tree-view.decorator';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
import { TreeNode } from 'primevue/tree';
import { toRaw } from 'vue';
import { sectionsHttpService } from '../infrastructure/sections.http-service';
import { ISectionModel } from '../models/ISectionModel';
import { SectionModel } from '../models/section.model';

export class SectionState extends StateBase {
  @collection
  @treeView((models: SectionModel[]) => {
    const modelToTree = (section: SectionModel): TreeNode => ({
      label: section.title.value as string,
      key: `${section.key}`,
      children: models.filter((m) => m.parentSectionId === section.id).map(modelToTree),
    });

    const nodes = models
      .filter((s) => s.parentSectionId == null || s.parentSectionId < 0)
      .map(modelToTree);
    console.log({
      aray: toRaw(models),
      nodes,
    });

    return nodes;
  })
  items: SectionModel[] | null = null;

  @dataStatus
  status = new DataStatus();

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
) as ICollectionStoreRead<ISectionModel, SectionModel> & ISelectedItem<ISectionModel, SectionModel>;
