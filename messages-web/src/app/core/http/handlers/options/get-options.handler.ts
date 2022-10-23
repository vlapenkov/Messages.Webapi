import { createHandler, Handler } from '@/app/core/handlers/handler';

export interface IRequestOptions {
  url: string;
}

export type OptionsGetter<TModel = undefined> = Handler<Partial<IRequestOptions>, TModel>;

/**
 *  Конфигурирует опции текущего запроса
 *  в зависимости от тела самого запроса
 * @example
 * const getOptions: OptionsGetter<{ foo: string }> = setAxiosOptonsFor(({ foo }) => ({
 *   url: `bar/${foo}`,
 * }));
 *
 * const url: IRequestOptions = getOptions({ foo: 'baz' });
 */
export const setAxiosOptonsFor = createHandler(
  () =>
    <TModel>(getter: OptionsGetter<TModel>) =>
    (arg: TModel) =>
      getter(arg),
);
