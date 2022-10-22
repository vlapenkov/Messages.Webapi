import { createHandler, Handler } from '@/app/core/handlers/handler';

export type UrlExtractor<TModel = undefined> = Handler<string, TModel>;

/** Создаёт генератор Url-ов для конкретного типа данных.
 *  Для поиска по Id и прочего такого
 * @example
 * const getUrl: UrlExtractor<{ foo: string }> = createUrlExtractor(
 *  (model: { foo: string }) => `bar/${model.foo}`
 * );
 *
 * const url: string = getUrl({foo: 'baz'})
 */
export const createUrlExtractor = createHandler(
  () =>
    <TModel>(getter: UrlExtractor<TModel>) =>
    (arg: TModel) =>
      getter(arg),
);
