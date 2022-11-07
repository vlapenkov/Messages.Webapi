import { Constructor } from '@/app/@types/constructor';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionHttpService } from '../../../http/custom/collection.http-service';
import { StateBase } from '../../state/base/state-base';

export function definePageableCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
  TState extends StateBase,
>(
  _name: string,
  _Model: Constructor<TModel>,
  _State: Constructor<TState>,
  _service: ICollectionHttpService<TIModel>,
) {
  throw new Error('Not Implemented!');
}
