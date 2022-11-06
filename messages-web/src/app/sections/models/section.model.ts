import { IModelUnique, modelMarker } from '@/app/core/models/@types/IModel';
import { description } from '@/app/core/models/decorators/description.decorator';
import { hidden } from '@/app/core/models/decorators/hidden.decorator';
import { mock } from '@/app/core/models/decorators/mock.decorator';
import { randomDate } from '@/app/core/models/decorators/mocks/random-date.mock';
import { randomRepeatableNumber } from '@/app/core/models/decorators/mocks/random-number.repeatable.mock';
import { randomUniqueNumber } from '@/app/core/models/decorators/mocks/random-number.unique.mock';
import {
  combine,
  randomName,
  randomWord,
} from '@/app/core/models/decorators/mocks/random-word.mock';
import { adjectives } from '@/app/core/models/decorators/mocks/words/adjectives';
import { render } from '@/app/core/models/decorators/render.decorator';
import { title } from '@/app/core/models/decorators/tittle.decorator';
import { validate } from '@/app/core/models/decorators/validate.decorator';
import { UniqueModel } from '@/app/core/models/unique.model';
import ParentSectionSelectorVue from '@/vue/containers/ParentSectionSelector.vue';
import { required } from '@vuelidate/validators';
import { h } from 'vue';

export interface ISectionModel extends IModelUnique<number> {
  name: string;
  parentSectionId: number | null;
  createdBy: string | null;
  lastModifiedBy: string | null;
  created: string;
  lastModified: string;
}
export class SectionModel extends UniqueModel<number, ISectionModel> {
  @hidden()
  @mock(randomUniqueNumber(1, 1000))
  id = -1;

  @title
  @mock(combine(randomWord(adjectives), randomWord()))
  @validate({ required })
  name = '';

  @hidden('view')
  @description('Родитель')
  @mock(randomRepeatableNumber(1, 100))
  @render(() => h(ParentSectionSelectorVue), 'edit')
  parentSectionId: number | null = null;

  @hidden('edit')
  @description('Создал:')
  @mock(randomName)
  @render((m: SectionModel) => m.createdBy ?? 'неизвестный пользователь')
  createdBy: string | null = null;

  @hidden('edit')
  @description('Внёс последние изменения')
  @mock(randomName)
  @render((m: SectionModel) => m.lastModifiedBy ?? 'неизвестный пользователь')
  lastModifiedBy: string | null = null;

  @hidden('edit')
  @description('Дата создания')
  @mock(randomDate())
  @render((m: SectionModel) => (m.created ? m.created.toLocaleDateString() : 'неизвестна'))
  created: Date | null = null;

  @hidden('edit')
  @description('Дата последних изменений')
  @mock(randomDate())
  @render((m: SectionModel) =>
    m.lastModified ? m.lastModified.toLocaleDateString() : 'неизвестна',
  )
  lastModified: Date | null = null;

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
