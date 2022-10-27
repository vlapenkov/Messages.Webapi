import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';

export interface ISectionModel extends IModel {
  id: number;
  name: string;
  parentSectionId: number;
}

export class SectionModel extends ModelBase<ISectionModel> implements ISectionModel {
  id = -1;

  name = '';

  parentSectionId = -1;

  tryParse(model: ISectionModel): boolean {
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

  equals(other: SectionModel): boolean {
    return (
      this.id === other.id &&
      this.name === other.name &&
      this.parentSectionId === other.parentSectionId
    );
  }
}
