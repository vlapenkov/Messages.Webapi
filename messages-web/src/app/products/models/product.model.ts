import { IModel, modelMarker } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { description } from '@/app/core/models/decorators/description.decorator';
import { hidden } from '@/app/core/models/decorators/hidden.decorator';
import { mock } from '@/app/core/models/decorators/mock.decorator';
import { randomDate } from '@/app/core/models/decorators/mocks/random-date.mock';
import { randomName } from '@/app/core/models/decorators/mocks/random-word.mock';
import { render } from '@/app/core/models/decorators/render.decorator';
import { title } from '@/app/core/models/decorators/tittle.decorator';

export interface IProductShortModel extends IModel {
  name: string;
  price: number;
  description: string;
  createdBy: string;
  lastModifiedBy: string;
  created: string;
  lastModified: string;
}

export class ProductShortModel extends ModelBase<IProductShortModel> {
  @description('Описание товара')
  description = '';

  @title
  name = '';

  @description('Цена')
  price: number | null = null;

  @hidden('always')
  @description('Создал:')
  @mock(randomName)
  @render((m: ProductShortModel) => m.createdBy ?? 'неизвестный пользователь')
  createdBy: string | null = null;

  @hidden('always')
  @description('Внёс последние изменения')
  @mock(randomName)
  @render((m: ProductShortModel) => m.lastModifiedBy ?? 'неизвестный пользователь')
  lastModifiedBy: string | null = null;

  @hidden('always')
  @description('Дата создания')
  @mock(randomDate())
  @render((m: ProductShortModel) => (m.created ? m.created.toLocaleDateString() : 'неизвестна'))
  created: Date | null = null;

  @hidden('always')
  @description('Дата последних изменений')
  @mock(randomDate())
  @render((m: ProductShortModel) =>
    m.lastModified ? m.lastModified.toLocaleDateString() : 'неизвестна',
  )
  lastModified: Date | null = null;

  get key(): string {
    return JSON.stringify({
      price: this.price,
      description: this.description,
    });
  }

  fromResponse(model: IProductShortModel): boolean {
    try {
      this.description = model.description;
      this.price = model.price;
      this.createdBy = model.createdBy;
      this.lastModifiedBy = model.lastModifiedBy;
      this.created = new Date(model.created);
      this.lastModified = new Date(model.lastModified);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IProductShortModel {
    return {
      created: this.created?.toUTCString() ?? '',
      lastModified: this.lastModified?.toUTCString() ?? '',
      createdBy: this.createdBy ?? '',
      lastModifiedBy: this.lastModifiedBy ?? '',
      description: this.description,
      price: this.price ?? 0,
      name: this.name,
      [modelMarker]: this[modelMarker],
    };
  }

  equals(mb: ProductShortModel): boolean {
    return mb.price === this.price && mb.description === this.description;
  }

  clone(): ProductShortModel {
    const cloned = new ProductShortModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
