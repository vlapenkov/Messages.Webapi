import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IStoreAdd } from '../../@types/IStoreAdd';
import { IStoreDelete } from '../../@types/IStoreDelete';
import { IStoreEdit } from '../../@types/IStoreEdit';
import { ICollectionStoreRead } from './ICollectionStoreRead';
import { IStoreSave } from '../../@types/IStoreSave';
import { ISelectedItem } from '../../@types/IStoreSelectedItem';
import { IStoreTree } from '../../@types/IStoreTree';

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
