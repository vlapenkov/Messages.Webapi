import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';
import { IPageableCollectionStore } from '@/app/core/services/harlem/custom-stores/pageable-collection/@types/IPageableCollectionStore';
import { IStoreAdd } from '../../@types/IStoreAdd';
import { IStoreDelete } from '../../@types/IStoreDelete';
import { IStoreEdit } from '../../@types/IStoreEdit';
import { IStoreSave } from '../../@types/IStoreSave';
import { ISelectedItem } from '../../@types/IStoreSelectedItem';
import { IStoreTree } from '../../@types/IStoreTree';

export type PageableCollectionStore<
  TIModel extends IModel,
  TModel extends ModelBase<TIModel>,
> = IPageableCollectionStore<TIModel, TModel> &
  Partial<IStoreAdd> &
  Partial<ISelectedItem<IModel, TModel>> &
  Partial<IStoreEdit> &
  Partial<IStoreAdd> &
  Partial<IStoreSave> &
  Partial<IStoreDelete> &
  Partial<IStoreTree>;
