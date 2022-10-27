import { IModelUnique } from '@/app/core/models/@types/IModel';
import { UniqueModel } from '@/app/core/models/unique.model';

export interface ISectionModel extends IModelUnique<number> {
  name: string;
  parentSectionId: number;
}

export class SectionModel extends UniqueModel<number, ISectionModel> implements ISectionModel {
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

  equalsDeep(other: SectionModel): boolean {
    return (
      this.id === other.id &&
      this.name === other.name &&
      this.parentSectionId === other.parentSectionId
    );
  }
}
