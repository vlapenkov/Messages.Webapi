import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/model-base';

export interface ISectionModel extends IModel {
  id: number;
  name: string;
  parentSectionId: number;
}

export class SectionModel extends ModelBase<ISectionModel> implements ISectionModel {
  id!: number;

  name!: string;

  parentSectionId!: number;

  tryParseModel(model: ISectionModel): boolean {
    try {
      const { id, name, parentSectionId } = model;
      if (id == null || name == null || parentSectionId == null) {
        return false;
      }
      this.id = id;
      this.name = name;
      this.parentSectionId = parentSectionId;
      return true;
    } catch (error) {
      return false;
    }
  }

  asObject(): ISectionModel {
    return this;
  }
}
