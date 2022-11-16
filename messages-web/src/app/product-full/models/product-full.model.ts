import { ModelBase } from '@/app/core/models/base/model-base';
import { description } from '@/app/core/models/decorators/description.decorator';
import { hidden } from '@/app/core/models/decorators/hidden.decorator';
import { render } from '@/app/core/models/decorators/render.decorator';
import { title } from '@/app/core/models/decorators/tittle.decorator';
import { h } from 'vue';
import ProductFileEdifor from '@/vue/containers/product-file-editor.vue';
import { noLabel } from '@/app/core/models/decorators/no-label.decorator';
import { IProductFullModel } from '../@types/IProductFullModel';
import { IProductDocument } from '../@types/IProductDocument';

export class ProductFullModel extends ModelBase<IProductFullModel> implements IProductFullModel {
  @hidden()
  id = 0;

  @title
  @description('Название')
  name = '';

  @description('Полное наименование')
  fullName = '';

  @description('Категория')
  @hidden('edit')
  catalogSectionId = 0;

  @description('Описание')
  description = '';

  @description('Код')
  codeTnVed = '';

  @description('Цена')
  price = 0;

  @description('Единицы измерения')
  measuringUnit = '';

  @description('Страна производства')
  country = '';

  @description('Валюта')
  currency = '';

  @hidden()
  status = 0;

  @hidden()
  @description('Атрибуты')
  attributeValues: { baseProductId: number; attributeId: number; value: string }[] = [];

  @description('Вложения')
  @noLabel
  @render(() => h(ProductFileEdifor), 'edit')
  documents: IProductDocument[] = [];

  fromResponse(model: IProductFullModel): boolean {
    try {
      Object.assign(this, model);
      return true;
    } catch (error) {
      return false;
    }
  }

  toRequest(): IProductFullModel {
    return this;
  }

  equals(mb: ProductFullModel): boolean {
    return (
      this.id === mb.id &&
      this.name === mb.name &&
      this.fullName === mb.fullName &&
      this.catalogSectionId === mb.catalogSectionId &&
      this.description === mb.description &&
      this.codeTnVed === mb.codeTnVed &&
      this.price === mb.price &&
      this.measuringUnit === mb.measuringUnit &&
      this.country === mb.country &&
      this.currency === mb.currency &&
      this.status === mb.status &&
      this.attributeValues.length === mb.attributeValues.length &&
      this.attributeValues.every((item, index) => {
        const { attributeId, baseProductId, value } = mb.attributeValues[index];
        return (
          item.attributeId === attributeId &&
          item.baseProductId === baseProductId &&
          item.value === value
        );
      }) &&
      this.documents.length === mb.documents.length &&
      this.documents.every((item, index) => {
        const { data, fileId, fileName } = mb.documents[index];
        return item.data === data && item.fileId === fileId && item.fileName === fileName;
      })
    );
  }

  get key(): string | number | symbol {
    return this.id;
  }

  clone(): ProductFullModel {
    const cloned = new ProductFullModel();
    Object.assign(cloned, this);
    return cloned;
  }
}
