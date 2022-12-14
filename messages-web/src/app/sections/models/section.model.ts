import { modelMarker } from '@/app/core/models/@types/IModel';
import { UniqueModel } from '@/app/core/models/unique.model';
import { ISectionModel } from './ISectionModel';

export class SectionModel extends UniqueModel<number, ISectionModel> {
  id = -1;

  name = '';

  parentSectionId: number | null = null;

  createdBy: string | null = null;

  lastModifiedBy: string | null = null;

  created: Date | null = null;

  lastModified: Date | null = null;

  documentId: string | null = null;

  fromResponse(m: ISectionModel): boolean {
    try {
      const { id, name, parentSectionId } = m;
      if (id == null || name == null) {
        return false;
      }
      this.id = id;
      this.name = name;
      this.parentSectionId = parentSectionId;
      this.created = new Date(m.created);
      this.lastModified = new Date(m.lastModified);
      this.createdBy = m.createdBy;
      this.lastModifiedBy = m.lastModifiedBy;
      this.documentId = m.documentId;
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): ISectionModel {
    return {
      id: this.id,
      created: this.created?.toUTCString() ?? '',
      createdBy: this.createdBy,
      lastModified: this.lastModified?.toUTCString() ?? '',
      lastModifiedBy: this.lastModifiedBy,
      name: this.name,
      parentSectionId: this.parentSectionId,
      documentId: this.documentId,
      [modelMarker]: this[modelMarker],
    };
  }

  equals(other: SectionModel): boolean {
    return (
      this.id === other.id &&
      this.name === other.name &&
      this.parentSectionId === other.parentSectionId
    );
  }

  clone(): SectionModel {
    const nm = new SectionModel();
    Object.assign(nm, this);
    return nm;
  }
}
