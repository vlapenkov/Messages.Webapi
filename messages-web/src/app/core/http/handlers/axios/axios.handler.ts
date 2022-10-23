import { createHandler, Handler } from '@/app/core/handlers/handler';
import { createWrapper } from '@/app/core/handlers/handler-wrapper';
import type { AxiosInstance, AxiosPromise } from 'axios';
import { http } from '../../http.service';
import type { IRequestOptions, OptionsGetter } from '../options/get-options.handler';

/** Произвольный запрос в axios */
export type AxiosHandlerFunction<TResponse, TRequest = undefined> = (
  http: AxiosInstance,
  request: TRequest,
) => AxiosPromise<TResponse>;

/** Подставляет текущий экземпляр axios в произвольный запрос
 * @example
 * // Создаём функцию, которая просит только данные в качестве аргумента
 * const findItem = createAxiosHandler((ax, id: string) =>
 *   ax.get<{ foo: string }>('find', {
 *     params: { id },
 *   }),
 * );
 *
 * // Получаем наши данные
 * const item: AxiosResponse<{
 *   foo: string;
 * }> = await findItem('some id');
 */
export const createAxiosHandler = createHandler(
  () =>
    <TResponse, TRequest = undefined>(handleRequest: AxiosHandlerFunction<TResponse, TRequest>) =>
    (request: TRequest) =>
      handleRequest(http, request),
);

/** Создаёт обёртку для подстановки произвольного урла в аксиос запрос
 * @example
 * interface IRequestTest {
 *   id: string;
 * }
 *
 * const wrapper = createAxiosWrapper<string, IRequestTest>(
 *   ({ url }) =>
 *     (ax, req) =>
 *       ax.get(url, { params: { ...req } }),
 * );
 *
 * const options = ({ id }: IRequestTest): IRequestOptions => ({
 *   url: `find/${id}`,
 * });
 *
 * const final: Handler<AxiosPromise<string>, IRequestTest> = extend(options).wrap(wrapper).done();
 *
 */
export function createAxiosWrapper<TResponse, TModel = undefined>(
  buildFn: (options: Partial<IRequestOptions>) => AxiosHandlerFunction<TResponse, TModel>,
) {
  return createWrapper<OptionsGetter<TModel>, Handler<AxiosPromise<TResponse>, TModel>>(
    (getOpts) => (requestModel) => {
      const opts = getOpts(requestModel);
      const requestFn = buildFn(opts);
      const handler = createAxiosHandler(requestFn);
      return handler(requestModel);
    },
  );
}
