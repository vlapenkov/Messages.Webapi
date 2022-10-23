import { createHandler, Handler } from '@/app/core/handlers/handler';
import { extend } from '@/app/core/handlers/handler-lab';
import { createWrapper } from '@/app/core/handlers/handler-wrapper';
import type { AxiosInstance, AxiosPromise } from 'axios';
import { http } from '../../http.service';
import type { UrlExtractor } from '../options/get-options.handler';

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
 *   (geturl) => (ax, req) => ax.get(geturl(req), { params: { ...req } }),
 * );
 *
 * const getUrl: UrlExtractor<IRequestTest> = ({ id }) => `find/${id}`;
 *
 * const final: Handler<AxiosPromise<string>, IRequestTest> = extend(getUrl).wrap(wrapper).done();
 *
 */
export function createAxiosWrapper<TResponse, TModel = undefined>(
  buildFn: (options: IRequestOptions<TModel>) => AxiosHandlerFunction<TResponse, TModel>,
) {
  return createWrapper<
    Handler<IRequestOptions<TModel>, TModel>,
    Handler<AxiosPromise<TResponse>, TModel>
  >((getOpts) => (requestModel) => {
    const opts = getOpts(requestModel);
    const requestFn = buildFn(opts);
    const handler = createAxiosHandler(requestFn);
    return handler(requestModel);
  });
}

interface IRequestTest {
  id: string;
}

const wrapper = createAxiosWrapper<string, IRequestTest>(
  ({ getUrl }) =>
    (ax, req) =>
      ax.get(getUrl(req), { params: { ...req } }),
);

const options = (req: IRequestTest): IRequestOptions<IRequestTest> => ({
  getUrl: ({ id }) => `find/${id}`,
});

const final: Handler<AxiosPromise<string>, IRequestTest> = extend(options).wrap(wrapper).done();
