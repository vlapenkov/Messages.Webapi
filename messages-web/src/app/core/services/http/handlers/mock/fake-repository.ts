import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';

export class FakeRepository<TIModel extends IModel, TModel extends ModelBase<TIModel>> {
  constructor(private Model: Constructor<TModel>, private size: number = 20) {}

  private $collection: TIModel[] | null = null;

  get collection(): TIModel[] {
    if (this.$collection != null) {
      return this.$collection;
    }
    const model = new this.Model();
    this.$collection = model.mockMany(this.size).map((m) => m.toRequest() as TIModel);
    return this.$collection;
  }
}
