import { IModel } from './@types/IModel';
import { ModelBase } from './base/model-base';

export abstract class UniqueModel<T extends IModel = IModel> extends ModelBase<T> {}
