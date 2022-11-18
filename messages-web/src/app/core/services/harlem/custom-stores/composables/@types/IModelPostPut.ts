import { IModel } from '@/app/core/models/@types/IModel';
import { RequetstHandler } from '@/app/core/services/http/@types/requetst-handler';

export interface IModelPostPut<TIModel extends IModel> {
  post: RequetstHandler<TIModel, TIModel>;
  put: RequetstHandler<TIModel, TIModel>;
}
