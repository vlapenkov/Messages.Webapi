import { StateBase } from '@/app/core/services/harlem/state/base/state-base';
import { DataStatus } from '@/app/core/services/harlem/tools/data-status';
import { NotValidData } from '@/app/core/services/harlem/tools/not-valid-data';
// import { toRaw } from 'vue';
import { SectionModel } from '../models/section.model';

export class SectionState extends StateBase {
  // @treeView((models: SectionModel[]) => {
  //   // console.log('models', toRaw(models));

  //   const modelToTree = (section: SectionModel): TreeNode => ({
  //     label: section.title.value as string,
  //     key: `${section.key}`,
  //     data: section,
  //     children: models.filter((m) => m.parentSectionId === section.id).map(modelToTree),
  //   });
  //   return models
  //     .filter((s) => s.parentSectionId == null || s.parentSectionId < 0)
  //     .map(modelToTree);
  // })
  items: SectionModel[] | null = null;

  status = new DataStatus();

  selectedItem: NotValidData<SectionModel> | null = null;
}
