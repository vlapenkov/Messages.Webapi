import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IStoreAdd } from './IStoreAdd';
import { IStoreDelete } from './IStoreDelete';
import { IStoreEdit } from './IStoreEdit';
import { ICollectionStoreRead } from './ICollectionStoreRead';
import { IStoreSave } from './IStoreSave';
import { ISelectedItem } from './IStoreSelectedItem';
import { IStoreTree } from './IStoreTree';

export type CollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = ICollectionStoreRead<IModel, TModel> &
  Partial<IStoreAdd> &
  Partial<ISelectedItem<IModel, TModel>> &
  Partial<IStoreEdit> &
  Partial<IStoreAdd> &
  Partial<IStoreSave> &
  Partial<IStoreDelete> &
  Partial<IStoreTree>;
