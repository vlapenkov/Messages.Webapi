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

  parseModel(model: ISectionModel): void {
    this.id = model.id;
    this.name = model.name;
    this.parentSectionId = model.parentSectionId;
  }

  asObject(): ISectionModel {
    return this;
  }
}
