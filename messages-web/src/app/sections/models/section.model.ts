import { IModelUnique } from '@/app/core/models/@types/IModel';
import { hidden } from '@/app/core/models/decorators/hidden.decorator';
import { title } from '@/app/core/models/decorators/tittle.decorator';
import { validate } from '@/app/core/models/decorators/validate.decorator';
import { UniqueModel } from '@/app/core/models/unique.model';
import { required } from '@vuelidate/validators';

export interface ISectionModel extends IModelUnique<number> {
  name: string;
  parentSectionId: number | null;
}
export class SectionModel extends UniqueModel<number, ISectionModel> implements ISectionModel {
  @hidden
  id = -1;

  @title
  @validate({ required })
  name = '';

  @hidden
  parentSectionId: number | null = null;

  tryParse(m: ISectionModel): boolean {
    try {
      const { id, name, parentSectionId } = m;
      if (id == null || name == null) {
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
