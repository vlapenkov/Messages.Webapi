import { createHandler } from '@/app/core/handlers/handler';
import { AxiosInstance, AxiosPromise } from 'axios';
import { http } from '../../http.service';

/** Произвольный запрос в axios */
export type AxiosHandlerFunction<TResponse, TRequest = undefined> = (
  http: AxiosInstance,
  request: TRequest,
) => AxiosPromise<TResponse>;

/** Подставляет текущий экземпляр axios в произвольный запрос
 * @example
 * // Создаём функцию, которая просит только данные в качестве аргумента
 * const findItem = axiosHandler((ax, id: string) =>
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
