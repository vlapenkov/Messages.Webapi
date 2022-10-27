import { Constructor } from '@/app/@types/constructor';
import { createHandler } from '@/app/core/handlers/base/handler';
import { HttpResult } from '@/app/core/handlers/http/results/base/http-result';
import { HttpStatus } from '@/app/core/handlers/http/results/base/http-status';
import { ErrorResult } from '@/app/core/handlers/http/results/error.result';
import { Ok } from '@/app/core/handlers/http/results/ok.result';
import { IModel } from '@/app/core/models/@types/IModel';
import { ModelBase } from '@/app/core/models/base/model-base';

export const parse = <TModel extends IModel, TModelClass extends ModelBase<TModel>>(
  Model: Constructor<TModelClass>,
) =>
  createHandler<Promise<HttpResult<TModelClass>>, Promise<HttpResult<TModel>>>(
    () => async (promise) => {
      const response = await promise;
      if (
        response.status === HttpStatus.Success &&
        response.data != null &&
        !Array.isArray(response.data)
      ) {
        const md = new Model();
        const parsed = md.tryParseModel(response.data);
        return parsed
          ? new Ok(md)
          : new ErrorResult<TModelClass>(
              'Не удалось преобразовать ответ от сервера к указанной модели',
            );
      }
      return new ErrorResult<TModelClass>(response.message ?? '');
    },
  );

export const parseArray = <TModel extends IModel, TModelClass extends ModelBase<TModel>>(
  Model: Constructor<TModelClass>,
) =>
  createHandler<Promise<HttpResult<TModelClass[]>>, Promise<HttpResult<TModel[]>>>(
    () => async (promise) => {
      const response = await promise;
      if (
        response.status === HttpStatus.Success &&
        response.data != null &&
        Array.isArray(response.data)
      ) {
        const resultData: TModelClass[] = [];
        for (let i = 0; i < response.data.length; i += 1) {
          const item = response.data[i];
          const md = new Model();
          const parsed = md.tryParseModel(item);
          if (!parsed) {
            return new ErrorResult<TModelClass[]>(
              'Не удалось преобразовать ответ от сервера к указанной модели',
            );
          }
          resultData.push(md);
        }
        return new Ok(resultData);
      }
      return new ErrorResult<TModelClass[]>(response.message ?? '');
    },
  );
