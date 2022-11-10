import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { ICollectionStoreAdd } from './ICollectionstoreAdd';
import { ICollectionStoreDelete } from './ICollectionStoreDelete';
import { ICollectionStoreEdit } from './ICollectionstoreEdit';
import { ICollectionStoreRead } from './ICollectionStoreRead';
import { ICollectionStoreSave } from './ICollectionStoreSave';
import { ICollectionStoreSelectedItem } from './ICollectionStoreSelectedItem';
import { ICollectionStoreTree } from './ICollectionStoreTree';

export type CollectionState<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = ICollectionStoreRead<IModel, TModel> &
  Partial<ICollectionStoreAdd> &
  Partial<ICollectionStoreSelectedItem<IModel, TModel>> &
  Partial<ICollectionStoreEdit> &
  Partial<ICollectionStoreAdd> &
  Partial<ICollectionStoreSave> &
  Partial<ICollectionStoreDelete> &
  Partial<ICollectionStoreTree>;
