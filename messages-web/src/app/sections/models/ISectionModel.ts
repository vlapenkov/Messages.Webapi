import { IModelUnique } from '@/app/core/models/@types/IModel';

export interface ISectionModel extends IModelUnique<number> {
  name: string;
  parentSectionId: number | null;
  createdBy: string | null;
  lastModifiedBy: string | null;
  created: string;
  lastModified: string;
  documentId: string | null;
}
